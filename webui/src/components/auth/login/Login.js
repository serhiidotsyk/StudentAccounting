import React, { useState } from "react";
import { connect } from "react-redux";
import { login } from "../../../actions/authAction";

import { Form, Input, Button } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";
import "./Login.css";
import { Redirect } from "react-router-dom";


const Login = props => {
  const [formRegister] = Form.useForm();
  const [done, setDone] = useState(false);

  const onSubmitForm = loginForm => {
    console.log(loginForm);
    props
      .login(loginForm)
      .then(() => setDone(true))
  };

  const form = (
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
        <Button type="primary" htmlType="submit" className="login-form-button">
          Log in
        </Button>
        Or <a href="/register">register now!</a>
      </Form.Item>
    </Form>
  );
  return done ? <Redirect to="/student/courses" /> : form;
};


export default connect(null, { login })(Login);
