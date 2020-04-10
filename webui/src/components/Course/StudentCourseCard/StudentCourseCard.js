import React from "react";
import { Card, Col, Button, Row } from "antd";
import { connect } from "react-redux";
import { unSubToCourse } from "../../../actions/courseAction";

const StudentCourseCard = (props) => {
  console.log(props);
  const course = props.course;
  const studentId = props.studentId;

  const dateConvert = (formatDate) => {
    return new Date(formatDate.concat("Z")).toLocaleString();
  };

  const unSubscribeFromCourse = (courseId, studentId) => {
    props.unSubToCourse({ courseId, studentId });
  };

  return (
    <Col sm={12} lg={8} xl={6}>
      <Card title={course.name} bordered={true}>
        Duration of course in days: {course.durationDays}
        <br></br>
        Course start : {dateConvert(course.startDate)}
        <br></br>
        Course end : {dateConvert(course.endDate)}
      </Card>
      <Row justify="center">
        <Button onClick={() => unSubscribeFromCourse(course.id, studentId)}>
          Unsubscribe
        </Button>
      </Row>
    </Col>
  );
};

export default connect(null, { unSubToCourse })(StudentCourseCard);
