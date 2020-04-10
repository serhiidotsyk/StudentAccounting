import React, { useState } from "react";
import { Card, Col, Row, Button, DatePicker } from "antd";
import { connect } from "react-redux";
import { subToCourse } from "../../../actions/courseAction";
import moment from "moment";
import { openNotification } from "../../../services/notifications";

const CourseCard = (props) => {
  const course = props.course;
  const studentId = props.studentId;
  const [enrollmentDate, setEnrollmentDate] = useState(0);
  const [disableButton, setDisableButton] = useState(true);

  const subscribeToCourse = (courseId, enrollmentDate) => {
    if (enrollmentDate == null) {
      openNotification.error("Please select date");
    } else {
      props.subToCourse({ studentId, courseId, enrollmentDate });
    }
  };

  const chooseDate = (e) => {
    setEnrollmentDate(e._d.valueOf());
    setDisableButton(false);
  };
  const disabledDate = (current) => {
    // Can not select days before today and today
    return current && current < moment().endOf("day");
  };

  return (
    <Col sm={12} lg={8} xl={6}>
      <Card title={course.name} bordered={true}>
        Duration of course in days: {course.durationDays}
      </Card>
      <Row justify="space-around">
        <DatePicker
          format="YYYY-MM-DD HH:mm:ss"
          disabledDate={disabledDate}
          showTime={{ defaultValue: moment("00:00:00", "HH:mm:ss") }}
          onOk={chooseDate}
        />
        <Button
          disabled={disableButton}
          onClick={() => subscribeToCourse(course.id, enrollmentDate)}>
          Subscribe
        </Button>
      </Row>
    </Col>
  );
};

export default connect(null, { subToCourse })(CourseCard);
