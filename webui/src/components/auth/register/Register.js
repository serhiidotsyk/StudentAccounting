import React, { useState } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import { register } from "../../../actions/authAction";

import { Form, Input, Button } from "antd";
import "./Register.css";

const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 8 }
  },
  wrapperCol: {
    xs: { span: 24,},
    sm: { span: 16 },
  }
};
const tailFormItemLayout = {
  wrapperCol: {
    xs: {
      span: 24,
      offset: 0
    },
    sm: {
      span: 16,
      offset: 8
    }
  }
};

function validateNumber(number) {
  if (!number || !number.trim()) {
    return undefined;
  }
  let num = Number(number);
  // num === ' '
  if (!isNaN(num)) {
    num = parseInt(number, 10);
  }
  return isNaN(num) ? number : num;
}

const Register = props => {
  const [formRegister] = Form.useForm();
  const [done, setDone] = useState(false);

  const onSubmitForm = regModel => {
    regModel.age = validateNumber(regModel.age);
    console.log(regModel);
    props.register(regModel).then(() => setDone(true));
  };  

  const form = (
    <Form
      className="register-form"  
      {...formItemLayout}
      form={formRegister}
      name="register"
      onFinish={onSubmitForm}
      
      scrollToFirstError>
      <Form.Item
        name="firstName"
        label="First Name"
        rules={[
          {
            required: true,
            message: "Please input your First Name!"
          }
        ]}>
        <Input/>
      </Form.Item>
      <Form.Item
        name="lastName"
        label="Last Name"
        rules={[
          {
            required: true,
            message: "Please input your Last Name!"
          }
        ]}>
        <Input/>
      </Form.Item>
      <Form.Item
        name="age"
        label="Age"
        rules={[
          {
            type: "number",
            transform: validateNumber,
            message: "Age is not a number"
          },
          {
            required: true,
            message: "Please input your Age!"
          },
          () => ({
            validator(rule, value) {
              if (value > 17 && value < 61) {
                return Promise.resolve();
              }
              return Promise.reject(
                "You do not match our age restrictions"
              );
            }
          })
        ]}>
        <Input/>
      </Form.Item>
      <Form.Item
        name="email"
        label="E-mail"
        rules={[
          {
            type: "email",
            message: "The input is not valid E-mail!"
          },
          {
            required: true,
            message: "Please input your E-mail!"
          }
        ]}>
        <Input/>
      </Form.Item>

      <Form.Item
        name="password"
        label="Password"
        rules={[
          {
            required: true,
            message: "Please input your password!"
          }
        ]}
        hasFeedback>
        <Input.Password/>
      </Form.Item>

      <Form.Item
        name="confirm"
        label="Confirm Password"
        dependencies={["password"]}
        hasFeedback
        rules={[
          {
            required: true,
            message: "Please confirm your password!"
          },
          ({ getFieldValue }) => ({
            validator(rule, value) {
              if (!value || getFieldValue("password") === value) {
                return Promise.resolve();
              }
              return Promise.reject(
                "The two passwords that you entered do not match!"
              );
            }
          })
        ]}>
        <Input.Password />
      </Form.Item>

      <Form.Item {...tailFormItemLayout}>
        <Button type="primary" htmlType="submit">
          Register
        </Button>
      </Form.Item>
    </Form>
  );
  return done ? <Redirect to="/login" /> : form;
};

export default connect(null, { register })(Register);
