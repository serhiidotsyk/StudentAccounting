import React, { useEffect, useState } from "react";
import StudentCourseCard from "./StudentCourseCard";
import { connect } from "react-redux";
import { getStudentCourses } from "../../../actions/courseAction";
import { Spin } from "antd";

const StudentCourseCards = ({ getStudentCourses, studentId, ...props }) => {
  const [isLoading, setIsLoading] = useState(false);

  useEffect(() => {
    getStudentCourses(studentId);
  }, [getStudentCourses, studentId]);

  const { studentCourses } = props.studentCourses;
  return (
    <Spin spinning={isLoading}>
    <div className="site-card-wrapper">
      <StudentCourseCard
        courses={studentCourses}
        studentId={parseInt(studentId, 10)}
        isLoading={isLoading}
        setIsLoading={setIsLoading}></StudentCourseCard>
    </div>
    </Spin>
  );
};

const mapStateToProps = state => {
  return {
    studentCourses: state.studentCourses,
    studentId: state.auth.user.id
  };
};

export default connect(mapStateToProps, { getStudentCourses })(
  StudentCourseCards
);
