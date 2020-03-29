import React from "react";
import { Card, Col, Row, Button, DatePicker } from "antd";
import { connect } from "react-redux";
import { subToCourse } from "../../../actions/courseAction";
import moment from "moment";

const CourseCard = props => {
  var myDate = null;
  const courses = props.courses;
  const studentId = props.studentId;

  const subscribeToCourse = (courseId, enrollmentDate) => {
    console.log(courseId);
    props.subToCourse({ studentId, courseId, enrollmentDate });
  };

  const chooseDate = e => {
    console.log(studentId);
    myDate = e._d.valueOf();
    console.log(myDate);
  };
  const disabledDate = current => {
    // Can not select days before today and today
    return current && current < moment().endOf("day");
  };

  return (
    <Row gutter={10}>
      {courses
        ? courses.map(course => (
            <Col key={course.id}>
              <Card title={course.name} bordered={true}>
                Duration of course in days: {course.durationDays}
              </Card>
              <Row>
                <Col>
                  <Button onClick={() => subscribeToCourse(course.id, myDate)}>
                    Subscribe
                  </Button>
                </Col>
                <Col>
                  <DatePicker
                    format="YYYY-MM-DD HH:mm:ss"
                    disabledDate={disabledDate}
                    showTime={{ defaultValue: moment("00:00:00", "HH:mm:ss") }}
                    onOk={chooseDate}
                  />
                </Col>
              </Row>
            </Col>
          ))
        : ""}
    </Row>
  );
};

export default connect(null, { subToCourse })(CourseCard);
