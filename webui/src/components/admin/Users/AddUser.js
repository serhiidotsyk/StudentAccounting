import React, { useState } from "react";
import { connect } from "react-redux";
import { Redirect } from "react-router-dom";
import { addUser } from "../../../actions/userAction";
import "./AddUsers.css"

import { Form, Input, Button } from "antd";

const formItemLayout = {
  labelCol: {
    xs: { span: 24 },
    sm: { span: 8 }
  },
  wrapperCol: {
    xs: { span: 24 },
    sm: { span: 16 }
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

const AddUserForm = props => {
  const [formRegister] = Form.useForm();
  const [done, setDone] = useState(false);

  const onSubmitForm = addUserModel => {
    addUserModel.age = validateNumber(addUserModel.age);
    console.log(addUserModel);

    props.addUser(addUserModel).then(() => setDone(true));
  };

  const form = (
    <Form
      className="add-user-form"
      {...formItemLayout}
      form={formRegister}
      name="addUser"
      onFinish={onSubmitForm}
      scrollToFirstError>
      <Form.Item
        name="firstName"
        label="First Name"
        rules={[
          {
            required: true,
            message: "Please input  First Name!"
          }
        ]}>
        <Input />
      </Form.Item>
      <Form.Item
        name="lastName"
        label="Last Name"
        rules={[
          {
            required: true,
            message: "Please input  Last Name!"
          }
        ]}>
        <Input />
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
            message: "Please input  Age!"
          },
          () => ({
            validator(rule, value) {
              if (value > 17 && value < 61) {
                return Promise.resolve();
              }
              return Promise.reject("You do not match our age restrictions");
            }
          })
        ]}>
        <Input />
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
            message: "Please input  E-mail!"
          }
        ]}>
        <Input />
      </Form.Item>

      <Form.Item
        name="password"
        label="Password"
        rules={[
          {
            required: true,
            message: "Please input  password!"
          }
        ]}
        hasFeedback>
        <Input.Password />
      </Form.Item>

      <Form.Item
        name="confirm"
        label="Confirm Password"
        dependencies={["password"]}
        hasFeedback
        rules={[
          {
            required: true,
            message: "Please confirm  password!"
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
          Add User
        </Button>
      </Form.Item>
    </Form>
  );
  return done ? <Redirect to="/admin/dashboard" /> : form;
};

export default connect(null, { addUser })(AddUserForm);
