import React, { useState } from "react";
import { connect } from "react-redux";
import { login, socialLogin } from "../../../actions/authAction";
import { Redirect } from "react-router-dom";
import FacebookLogin from "react-facebook-login";

import { Form, Input, Button } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import "./Login.css";

const Login = props => {
  console.log(window.FB);
  const [formRegister] = Form.useForm();
  const [done, setDone] = useState(false);

  const onSubmitForm = loginForm => {
    console.log(loginForm);
    props.login(loginForm).then(() => setDone(true));
  };
  const responseFacebook = response => {
    const postData = {
      Email: response.email,
      Name: response.name,
      BirthDate: response.birthday
    };
    console.log(postData);
    props.socialLogin(postData).then(() => {
      setDone(true);
      console.log(window.FB);
    });
  };

  const form = (
    <>
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
            placeholder="Password"
          />
        </Form.Item>

        <Form.Item
          name="password"
          rules={[{ required: true, message: "Please input your password!" }]}>
          <Input.Password
            prefix={<LockOutlined className="site-form-item-icon" />}
            placeholder="Password"
          />
        </Form.Item>
        <Form.Item>
          <Button
            type="primary"
            htmlType="submit"
            className="login-form-button">
            Log in
          </Button>
          Or <a href="/register">register now!</a>
        </Form.Item>
      </Form>
      <FacebookLogin
        appId="582075492402078"
        fields="name,email,birthday"
        callback={responseFacebook}
        cookie={true}
        icon="fa-facebook"
      />
    </>
  );
  return done ? <Redirect to="/student/courses" /> : form;
};

export default connect(null, { login, socialLogin })(Login);
