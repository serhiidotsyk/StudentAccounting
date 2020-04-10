import axios from "axios";
import { GET_ALL_USERS, UPDATE_USER, DELETE_USER, DELETE_USERS } from "./types";

import {
  API_GET_ALL_USERS,
  API_UPDATE_USER,
  API_DELETE_USER,
  API_DELETE_USERS,
} from "../config";

import { trackPromise } from "react-promise-tracker";
import { openNotification } from "../services/notifications"

export function getUsers(users, count) {
  return {
    type: GET_ALL_USERS,
    users,
    count,
  };
}

export function updateUser(user) {
  return {
    type: UPDATE_USER,
    user,
  };
}

export function deleteUser(user) {
  return {
    type: DELETE_USER,
    user,
  };
}

export function deleteUsers(users) {
  return {
    type: DELETE_USERS,
    users,
  };
}

const dateConvert = (formatDate) => {
  return new Date(formatDate.concat("Z")).toLocaleString();
};

export function getAllUsers({
  pageNumber,
  pageSize,
  searchString,
  sortOrder,
  sortField,
}) {
  return (dispatch) => {
    return trackPromise(
      axios
        .get(API_GET_ALL_USERS, {
          params: {
            pageNumber: pageNumber,
            pageSize: pageSize,
            searchString: searchString,
            sortOrder: sortOrder,
            sortField: sortField,
          },
        })
        .then((res) => {
          const users = res.data.student.map((user) => {
            user.courses.map((course) => {
              course.startDate = dateConvert(course.startDate);
              course.endDate = dateConvert(course.endDate);
              return course;
            });
            return user;
          });
          const count = res.data.count;
          dispatch(getUsers(users, count));
        })
    );
  };
}

export function updateStudent(user) {
  console.log("user", user);
  return (dispatch) => {
    return trackPromise(
      axios.put(API_UPDATE_USER, user).then((res) => {
        openNotification.success("Successfuly updated student");
        dispatch(updateUser(res.data));
      })
    );
  };
}

export function deleteStudent(studentId) {
  return (dispatch) => {
    return trackPromise(
      axios
        .delete(API_DELETE_USER, { params: { studentId: studentId } })
        .then((res) => {
          dispatch(deleteUser(res.data));
          openNotification.success("Successfuly deleted user");
        })
    );
  };
}

export function deleteStudents(studentIds) {
  return (dispatch) => {
    return trackPromise(
      axios
        .delete(API_DELETE_USERS, { params: { studentIds: studentIds } })
        .then((res) => {
          dispatch(deleteUser(res.data));
          openNotification.success("Successfuly deleted user");
        })
    );
  };
}
