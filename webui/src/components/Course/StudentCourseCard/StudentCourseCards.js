import React, { useEffect } from "react";
import StudentCourseCard from "./StudentCourseCard";
import { connect } from "react-redux";
import { getStudentCourses } from "../../../actions/courseAction";

const StudentCourseCards = ({ getStudentCourses, studentId, ...props }) => {
  useEffect(() => {
    getStudentCourses(studentId);
  }, [getStudentCourses, studentId]);

  const { studentCourses } = props.studentCourses;
  return (
    <div className="site-card-wrapper">
      <StudentCourseCard courses={studentCourses} studentId={parseInt(studentId, 10)}></StudentCourseCard>
    </div>
  );
};

const mapStateToProps = state => {
  return {
    studentCourses: state.studentCourses,
    studentId: state.auth.user.id
  };
};

export default connect(mapStateToProps, { getStudentCourses })(StudentCourseCards);
