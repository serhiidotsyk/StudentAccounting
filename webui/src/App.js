import React, { useState } from "react";
import { Route, withRouter, Redirect } from "react-router";
import { BrowserRouter as Router, Link, NavLink } from "react-router-dom";
import { connect } from "react-redux";

import { usePromiseTracker } from "react-promise-tracker";

import Home from "./components/home/Home";
import Login from "./components/auth/login/Login";
import Register from "./components/auth/register/Register";
import CourseCards from "./components/Course/CourseCards/CourseCards";
import StudentCourseCards from "./components/Course/StudentCourseCard/StudentCourseCards";
import Dashboard from "./components/admin/Dashboard";

import {
  checkAuthRoleStudent,
  checkAuthRoleAdmin,
} from "./utils/checkAuthRoleHelper";
import { logout } from "./actions/authAction";

import { Layout, Menu, Row, Col, Spin, Drawer, Button } from "antd";
import { MenuUnfoldOutlined, MenuFoldOutlined } from "@ant-design/icons";
import logo from "./logo.svg";
import "./App.css";
import AddUserFrom from "./components/admin/Users/AddUser";

const { Header, Content, Footer } = Layout;

const App = (props) => {
  const { promiseInProgress } = usePromiseTracker();
  const [visible, setVisible] = useState(false);

  const logout = () => {
    props.logout();

    // window.FB.logout(function (response) {
    //   console.log(response);
    // });
  };
  const onClose = () => {
    setVisible(false);
  };

  const showDrawer = () => {
    setVisible(true);
  };

  const DesktopHeader = () => {
    return (
      <Header align="end">
        <Row justify="start">
          <Col xs={4} md={0}>
            {/* className="expandButtonWrapper" */}
            <Button onClick={showDrawer}>
              {React.createElement(
                visible ? MenuUnfoldOutlined : MenuFoldOutlined
              )}
            </Button>
          </Col>
          <Col xs={0} md={1}>
            <Link to="/">
              <img src={logo} alt="logo" />
            </Link>
          </Col>
          <Col md={6} lg={4} style={{ color: "#ffffff" }}>
            {props.auth.user.email}
          </Col>
          <Col xs={0} md={24} flex="auto">
            <Menu
              theme="dark"
              mode="horizontal"
              selectedKeys={null}
              style={{ lineHeight: "64px" }}>
              {checkAuthRoleStudent(isAuthenticated, userRole)
                ? [
                    <Menu.Item key="my-courses">
                      <Link to="/student/my-courses">My Courses</Link>
                    </Menu.Item>,
                    <Menu.Item key="courses">
                      <Link to="/student/courses">Courses</Link>
                    </Menu.Item>,
                    <Menu.Item key="logoutStudent" onClick={logout}>
                      <Link to="/login">Logout</Link>
                    </Menu.Item>,
                  ]
                : checkAuthRoleAdmin(isAuthenticated, userRole)
                ? [
                    <Menu.Item key="dashboard">
                      <Link
                        to="/admin/dashboard"
                        onClick={() => window.location.reload()}>
                        Dashboard
                      </Link>
                    </Menu.Item>,
                    <Menu.Item key="logoutAdmin" onClick={logout}>
                      <Link to="/login">Logout</Link>
                    </Menu.Item>,
                  ]
                : [
                    <Menu.Item key="register">
                      <Link to="/register">Register</Link>
                    </Menu.Item>,
                    <Menu.Item key="login">
                      <NavLink to="/login">Login</NavLink>
                    </Menu.Item>,
                  ]}
            </Menu>
          </Col>
        </Row>
      </Header>
    );
  };

  const onMobileMenuSelect = () => {
    onClose();
  };
  const ModileHeader = () => {
    return (
      <Menu
        mode="vertical"
        selectedKeys={null}
        style={{ lineHeight: "64px" }}
        onSelect={onMobileMenuSelect}>
        {checkAuthRoleStudent(isAuthenticated, userRole)
          ? [
              <Menu.Item key="my-courses">
                <Link to="/student/my-courses">My Courses</Link>
              </Menu.Item>,
              <Menu.Item key="courses">
                <Link to="/student/courses">Courses</Link>
              </Menu.Item>,
              <Menu.Item key="logoutStudent" onClick={logout}>
                <Link to="/login">Logout</Link>
              </Menu.Item>,
            ]
          : checkAuthRoleAdmin(isAuthenticated, userRole)
          ? [
              <Menu.Item key="dashboard">
                <Link
                  to="/admin/dashboard"
                  onClick={() => window.location.reload()}>
                  Dashboard
                </Link>
              </Menu.Item>,
              <Menu.Item key="logoutAdmin" onClick={logout}>
                <Link to="/login">Logout</Link>
              </Menu.Item>,
            ]
          : [
              <Menu.Item key="register">
                <Link to="/register">Register</Link>
              </Menu.Item>,
              <Menu.Item key="login">
                <NavLink to="/login">Login</NavLink>
              </Menu.Item>,
            ]}
      </Menu>
    );
  };

  const { isAuthenticated } = props.auth;
  const userRole = props.auth.user.role;

  return (
    <Router>
      <Layout className="layout">
        <DesktopHeader></DesktopHeader>

        <Drawer
          title="Menu"
          placement="left"
          closable={true}
          onClose={onClose}
          visible={visible}>
          <ModileHeader />
        </Drawer>

        <Content style={{ minHeight: "100vh", overflow: "auto", backgroundColor:"white" }}>
          <Row direction="column" align="top" justify="center" className="site-layout-content">
            {/* <Col> */}
              <Spin wrapperClassName="site-layout-content" spinning={promiseInProgress}>
                {checkAuthRoleStudent(isAuthenticated, userRole)
                  ? [
                      <Route
                        key="home"
                        exact
                        path="/"
                        component={Home}></Route>,
                      <Route
                        key="studentAllCourses"
                        path="/student/courses"
                        component={CourseCards}></Route>,
                      <Route
                        key="studentCourses"
                        path="/student/my-courses"
                        component={StudentCourseCards}></Route>,
                      //TODO: fix redirect on empty course list
                      <Redirect
                        key="studentRedirectCourse"
                        to="/student/courses"
                      />,
                    ]
                  : checkAuthRoleAdmin(isAuthenticated, userRole)
                  ? [
                      <Route
                        key="adminDashboard"
                        path="/admin/dashboard"
                        component={Dashboard}></Route>,
                      <Route
                        key="adminAddUser"
                        path="/admin/addUser"
                        component={AddUserFrom}></Route>,
                      <Redirect
                        key="adminDashboardRedirect"
                        to="/admin/dashboard"
                      />,
                    ]
                  : [
                      <Route
                        key="login"
                        path="/login"
                        component={Login}></Route>,
                      <Route
                        key="register"
                        path="/register"
                        component={Register}></Route>,
                      <Redirect key="loginRedirect" to="/login" />,
                    ]}
              </Spin>
            {/* </Col> */}
          </Row>
        </Content>
        <Footer
          style={{
            textAlign: "center",
            
            
            bottom: "0px",
            width: "100%",
          }}>
          Ant Design ©2018 Created by Ant UED
        </Footer>
      </Layout>
    </Router>
  );
};

const mapStateToProps = (state) => {
  return {
    auth: state.auth,
  };
};

export default connect(mapStateToProps, { logout })(withRouter(App));
