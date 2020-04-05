import React from "react";
import { Card, Col, Row, Button} from "antd";
import { connect } from "react-redux";
import { unSubToCourse } from "../../../actions/courseAction"

const StudentCourseCard = props => {
  console.log(props);
  const courses = props.courses;
  const studentId = props.studentId;

  const { isLoading, setIsLoading } = props;
  const dateConvert = (formatDate) => {
    return new Date(formatDate.concat("Z")).toLocaleString();
  }
  const unSubscribeFromCourse = (courseId, studentId) => {
    setIsLoading(true);
    console.log(isLoading);
    props.unSubToCourse({courseId, studentId }).then(res =>{
      setIsLoading(false);
    })
  };

  return (
    <Row gutter={10}>
      {courses
        ? courses.map(course => ( 
            <Col key={course.id}>
              <Card title={course.name} bordered={true}>
                Duration of course in days: {course.durationDays}
                <br></br>
                Course start : {dateConvert(course.startDate)}
                <br></br>
                Course end : {dateConvert(course.endDate)}
              </Card>
              <Button onClick={() => unSubscribeFromCourse(course.id, studentId)}>Unsubscribe</Button>
            </Col>
          ))
        : ""}
    </Row>
  );
};

export default connect(null, { unSubToCourse })(StudentCourseCard);
