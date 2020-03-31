import React, { useEffect } from "react";
import CourseCard from "./CourseCard";
import { connect } from "react-redux";
import { getCourses } from "../../../actions/courseAction";
import "./CourseCards.css"

const CourseCards = ({ getCourses, studentId, ...props }) => {

  useEffect(() => {
    getCourses(studentId);
  }, [getCourses, studentId]);

  const { allCourses } = props.allCourses;
  return (
    <div className="site-card-wrapper">
      <CourseCard
        courses={allCourses}
        studentId={parseInt(studentId, 10)}></CourseCard>
    </div>
  );
};

const mapStateToProps = state => {
  return {
    allCourses: state.course,
    studentId: state.auth.user.id
  };
};

export default connect(mapStateToProps, { getCourses })(CourseCards);
