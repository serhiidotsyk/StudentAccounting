import axios from "axios";
import {
  GET_ALL_COURSES,
  SUBSCRIBE_TO_COURSE,
  UNSUBSCRIBE_TO_COURSE,
  GET_ALL_COURSES_BY_STUDENT,
} from "./types";

import {
  API_GET_AVAILABLE_COURSES,
  API_SUBSCRIBE_TO_COURSE,
  API_UNSUBSCRIBE_TO_COURSE,
  API_GET_SUBSCRIBED_COURSES,
} from "../config";

import { openNotification } from "../services/notifications";
import { trackPromise } from "react-promise-tracker";

export function getAllCourses(courses) {
  return {
    type: GET_ALL_COURSES,
    courses,
  };
}

export function subscribeToCourse(course) {
  return {
    type: SUBSCRIBE_TO_COURSE,
    course,
  };
}
export function unSubscribeToCourse(course) {
  return {
    type: UNSUBSCRIBE_TO_COURSE,
    course,
  };
}

export function getCoursesByStudent(courses) {
  return {
    type: GET_ALL_COURSES_BY_STUDENT,
    courses,
  };
}

export function getCourses(studentId) {
  return (dispatch) => {
    return trackPromise(
      axios
        .get(API_GET_AVAILABLE_COURSES, { params: { studentId: studentId } })
        .then((res) => {
          console.log(res);
          dispatch(getAllCourses(res.data));
        })
    );
  };
}

export function subToCourse(data) {
  return (dispatch) => {
    console.log(data);
    return trackPromise(
      axios.put(API_SUBSCRIBE_TO_COURSE, data).then((res) => {
        console.log(res);
        dispatch(subscribeToCourse(data));
        openNotification.success("Successfuly subscribed to course");
      })
    );
  };
}

export function unSubToCourse(data) {
  return (dispatch) => {
    console.log(data);
    return trackPromise(
      axios.put(API_UNSUBSCRIBE_TO_COURSE, data).then((res) => {
        console.log(res);
        dispatch(unSubscribeToCourse(data));
        openNotification.success("Successfuly unsubscribed to course");
      })
    );
  };
}

export function getStudentCourses(studentId) {
  console.log(studentId);
  return (dispatch) => {
    return trackPromise(
      axios
        .get(API_GET_SUBSCRIBED_COURSES, { params: { studentId: studentId } })
        .then((res) => {
          dispatch(getCoursesByStudent(res.data));
        })
    );
  };
}
