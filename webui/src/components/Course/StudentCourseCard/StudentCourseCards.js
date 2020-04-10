import React, { useEffect } from "react";
import StudentCourseCard from "./StudentCourseCard";
import { connect } from "react-redux";
import { getStudentCourses } from "../../../actions/courseAction";
import { Empty, Row } from "antd";

const StudentCourseCards = ({ getStudentCourses, studentId, ...props }) => {
  useEffect(() => {
    getStudentCourses(studentId);
  }, [getStudentCourses, studentId]);

  const { studentCourses } = props.studentCourses;
  return (
    <Row gutter={[10, 10]}>
      {studentCourses.length > 0 ? (
        studentCourses.map((course) => (
          <StudentCourseCard
            key={course.id}
            course={course}
            studentId={parseInt(studentId, 10)}></StudentCourseCard>
        ))
      ) : (
        <Empty />
      )}
    </Row>
  );
};

const mapStateToProps = (state) => {
  return {
    studentCourses: state.studentCourses,
    studentId: state.auth.user.id,
  };
};

export default connect(mapStateToProps, { getStudentCourses })(
  StudentCourseCards
);
