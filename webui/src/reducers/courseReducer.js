import { GET_ALL_COURSES, SUBSCRIBE_TO_COURSE } from "../actions/types";

const intitialState = {
  allCourses: []
};

export default (state = intitialState, action = {}) => {
  switch (action.type) {
    case GET_ALL_COURSES:
      return {
        ...state,
        allCourses: action.courses
      };
    case SUBSCRIBE_TO_COURSE:
      return {
        allCourses: state.allCourses.filter(
          ({ id }) => id !== action.course.courseId
        )
      };
    default:
      return state;
  }
};
