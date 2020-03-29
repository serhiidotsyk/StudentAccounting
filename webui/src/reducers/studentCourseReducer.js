import {
  GET_ALL_COURSES_BY_STUDENT,
  UNSUBSCRIBE_TO_COURSE
} from "../actions/types";

const intitialState = {
  studentCourses: []
};

export default (state = intitialState, action = {}) => {
  switch (action.type) {
    case UNSUBSCRIBE_TO_COURSE:
      return {
        studentCourses: state.studentCourses.filter(
          ({ id }) => id !== action.course.courseId
        )
      };
    case GET_ALL_COURSES_BY_STUDENT:
      return {
        studentCourses: action.courses
      };

    default:
      return state;
  }
};
