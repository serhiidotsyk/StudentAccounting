import React, { useEffect } from "react";
import CourseCard from "./CourseCard";
import { connect } from "react-redux";
import { getCourses } from "../../../actions/courseAction";
import { Empty, Row } from "antd";

const CourseCards = ({ getCourses, studentId, ...props }) => {
  useEffect(() => {
    getCourses(studentId);
  }, [getCourses, studentId]);

  const { allCourses } = props.allCourses;

  return (
    <Row gutter={[10,10]}>
      {allCourses.length > 0 ? (
        allCourses.map((course) => <CourseCard key={course.id} course={course} studentId={parseInt(studentId, 10)}/>)
      ) : (
        <Empty />
      )}
    </Row>
  );
};

const mapStateToProps = (state) => {
  return {
    allCourses: state.course,
    studentId: state.auth.user.id,
  };
};

export default connect(mapStateToProps, { getCourses })(CourseCards);
