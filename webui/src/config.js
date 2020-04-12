export const BASE_API_URL = "https://localhost:44358/api/";

export const API_LOGIN = BASE_API_URL + "auth/signIn";
export const API_SOCIAL_LOGIN = BASE_API_URL + "auth/socialLogin";
export const API_REGISTER = BASE_API_URL + "auth/register";

export const API_GET_COURSES = BASE_API_URL + "course/getCourses";

export const API_GET_AVAILABLE_COURSES =
  BASE_API_URL + "course/getAvailableCourses";
export const API_GET_SUBSCRIBED_COURSES =
  BASE_API_URL + "course/getCoursesByStudentId";

export const API_SUBSCRIBE_TO_COURSE =
  BASE_API_URL + "course/subscribeToCourse";
export const API_UNSUBSCRIBE_TO_COURSE =
  BASE_API_URL + "course/unSubscribeFromCourse";

export const API_GET_ALL_USERS = BASE_API_URL + "admin/getAllStudents";
export const API_UPDATE_USER = BASE_API_URL + "admin/updateStudent";
export const API_DELETE_USER = BASE_API_URL + "admin/deleteStudent";
export const API_DELETE_USERS = BASE_API_URL + "admin/deleteStudents";

export const API_ADD_USER = BASE_API_URL + "admin/createStudent";
