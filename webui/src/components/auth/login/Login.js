import React, { useState } from "react";
import { connect } from "react-redux";
import { login, socialLogin } from "../../../actions/authAction";
import { Redirect, Link } from "react-router-dom";
import FacebookLogin from "react-facebook-login";

import { Form, Input, Button, Col, Row } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import "./Login.css";

const Login = (props) => {
  console.log(window.FB);
  const [formRegister] = Form.useForm();
  const [done, setDone] = useState(false);

  const onSubmitForm = (loginForm) => {
    console.log(loginForm);
    props.login(loginForm).then(() => setDone(true));
  };
  const responseFacebook = (response) => {
    console.log("hello");
    const postData = {
      Email: response.email,
      Name: response.name,
      BirthDate: response.birthday,
    };
    console.log(postData);
    props.socialLogin(postData).then(
      (res) => {
        setDone(true);
        console.log(done);
        console.log(window.FB);
      },
      (error) => {
        console.log(error);
      }
    );
    console.log(done);
  };

  const form = (
    <Row allign="middle" justify="center" style={{paddingTop:"100px"}}>
      <Col md={8} sm={18} xl={5}>
        <Form
          name="login"
          className="login-form"
          onFinish={onSubmitForm}
          form={formRegister}>
          <Form.Item
            name="email"
            rules={[{ required: true, message: "Please input your email!" }]}>
            <Input
              prefix={<UserOutlined className="site-form-item-icon" />}
              placeholder="Email"
            />
          </Form.Item>

          <Form.Item
            name="password"
            rules={[
              { required: true, message: "Please input your password!" },
            ]}>
            <Input.Password
              prefix={<LockOutlined className="site-form-item-icon" />}
              placeholder="Password"
            />
          </Form.Item>
          <Form.Item>
              <Col>
                <Button
                  style={{ width: "100%" }}
                  type="primary"
                  htmlType="submit"
                  className="login-form-button">
                  Log in
                </Button>
              </Col>
              <Col>
                <Link to="/register">Register now!</Link>
              </Col>
          </Form.Item>
        </Form>
        <Col >
        <FacebookLogin
          appId="582075492402078"
          fields="name,email,birthday"
          callback={responseFacebook}
          cookie={true}
          icon="fa-facebook"
          size="small"
          textButton="Facebook"
        />
        </Col>
      </Col>
    </Row>
  );
  return done ? <Redirect to="/student/courses" /> : form;
};

export default connect(null, { login, socialLogin })(Login);
