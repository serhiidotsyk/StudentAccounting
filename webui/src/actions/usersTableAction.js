import axios from "axios";
import { 
  GET_ALL_USERS, 
  UPDATE_USER, 
  DELETE_USER,
  DELETE_USERS
} from "./types";

import { 
  API_GET_ALL_USERS, 
  API_UPDATE_USER, 
  API_DELETE_USER,
  API_DELETE_USERS 
} from "../config";

export function getUsers(users) {
  return {
    type: GET_ALL_USERS,
    users
  };
}

export function updateUser(user) {
  return {
    type: UPDATE_USER,
    user
  };
}

export function deleteUser(user) {
  return {
    type: DELETE_USER,
    user
  };
}

export function deleteUsers(users) {
  return {
    type: DELETE_USERS,
    users
  };
}

export function getAllUsers() {
  return dispatch => {
    return axios.get(API_GET_ALL_USERS).then(res => {
      dispatch(getUsers(res.data));
    });
  };
}

export function updateStudent(user) {
  console.log("user", user);
  return dispatch => {
    return axios.put(API_UPDATE_USER, user).then(res => {
      dispatch(updateUser(res.data));
    });
  };
}

export function deleteStudent(studentId) {
  return dispatch => {
    return axios.delete(API_DELETE_USER, { params:{ studentId: studentId}}).then(res => {
      dispatch(deleteUser(res.data));
    })
  };
}

export function deleteStudents(studentIds) {
  return dispatch => {
    return axios.delete(API_DELETE_USERS, { params:{ studentId: studentIds}}).then(res => {
      dispatch(deleteUser(res.data));
    })
  };
}
