import axios from "axios";
import { 
  GET_ALL_COURSES, 
  SUBSCRIBE_TO_COURSE,
  UNSUBSCRIBE_TO_COURSE,
  GET_ALL_COURSES_BY_STUDENT 
}  from "./types";

import { 
  API_GET_AVAILABLE_COURSES, 
  API_SUBSCRIBE_TO_COURSE, 
  API_UNSUBSCRIBE_TO_COURSE,
  API_GET_SUBSCRIBED_COURSES
} from "../config";

export function getAllCourses(courses) {
  return {
    type: GET_ALL_COURSES,
    courses
  };
}

export function subscribeToCourse(course) {
  return {
    type: SUBSCRIBE_TO_COURSE,
    course
  }
}
export function unSubscribeToCourse(course)
{
  return {
    type: UNSUBSCRIBE_TO_COURSE,
    course
  }
}

export function getCoursesByStudent(courses)
{
  return {
    type: GET_ALL_COURSES_BY_STUDENT,
    courses
  }
}


export function getCourses(studentId) {
  console.log(studentId);
  return dispatch => {
    return axios.get(API_GET_AVAILABLE_COURSES, { params:{ studentId: studentId}}).then(res => {
      console.log(res);
      dispatch(getAllCourses(res.data))
    });
  };
}

export function subToCourse(data){
  return dispatch => {
    console.log(data);
    return axios.put(API_SUBSCRIBE_TO_COURSE, data).then(res => {
      console.log(res);
      dispatch(subscribeToCourse(data));
    });
  };
}

export function unSubToCourse(data){
  return dispatch => {
    console.log(data);
    return axios.put(API_UNSUBSCRIBE_TO_COURSE, data).then(res => {
      console.log(res);
      dispatch(unSubscribeToCourse(data));
    });
  };
}

export function getStudentCourses(studentId) {
  console.log(studentId);
  return dispatch => {
    return axios.get(API_GET_SUBSCRIBED_COURSES, { params:{ studentId: studentId}}).then(res => {
      console.log(res);
      dispatch(getCoursesByStudent(res.data))
    });
  };
}
