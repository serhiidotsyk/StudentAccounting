import React from "react";
import { Route, withRouter, Redirect } from "react-router";
import { BrowserRouter as Router, Link, NavLink } from "react-router-dom";
import { connect } from "react-redux";
import "./App.css";

import Home from "./components/home/Home";
import Login from "./components/auth/login/Login";
import Register from "./components/auth/register/Register";
import CourseCards from "./components/Course/CourseCards/CourseCards";
import MyCourseCards from "./components/Course/StudentCourseCard/StudentCourseCards";
//import AdminDashboard from "./components/Admin/Dashboard"
// import Profile from "./components/profile/Profile";
import {
  checkAuthRoleStudent,
  checkAuthRoleAdmin
} from "./utils/checkAuthRoleHelper";
import { logout } from "./actions/authAction";

import { Layout, Menu } from "antd";

import logo from "./logo.svg";

const { Header, Content, Footer } = Layout;

const App = props => {
  const logout = () => {
    props.logout();
  };

  const { isAuthenticated } = props.auth;
  const userRole = props.auth.user.role;

  return (
    <Router>
      <Layout className="layout">
        <Header align="end">
          <div className="logo">
            <Link to="/">
              <img src={logo} alt="logo" />
            </Link>
          </div>
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
                  <Menu.Item key="logoutStudent" onClick={logout.bind(this)}>
                    <Link to="/login">Logout</Link>
                  </Menu.Item>
                ]
              : checkAuthRoleAdmin(isAuthenticated, userRole)
              ? [
                  <Menu.Item key="dashboard">
                    <Link to="/admin/dashboard">My Courses</Link>
                  </Menu.Item>,
                  <Menu.Item key="logoutAdmin" onClick={logout.bind(this)}>
                    <Link to="/login">Logout</Link>
                  </Menu.Item>
                ]
              : [
                  <Menu.Item key="register">
                    <Link to="/register">Register</Link>
                  </Menu.Item>,
                  <Menu.Item key="login">
                    <NavLink to="/login">Login</NavLink>
                  </Menu.Item>
                ]}
          </Menu>
        </Header>
        <Content>
          <div align="center" className="site-layout-content">
            {checkAuthRoleStudent(isAuthenticated, userRole)
              ? [
                  <Route key="home" exact path="/" component={Home}></Route>,
                  <Route
                    key="studentAllCourses"
                    path="/student/courses"
                    component={CourseCards}></Route>,
                  <Route
                    key="studentCourses"
                    path="/student/my-courses"
                    component={MyCourseCards}></Route>,
                  <Redirect key="studentRedirectCourse" to="/student/courses" />
                ]
              : checkAuthRoleAdmin(isAuthenticated, userRole)
              ? [
                  <Route
                    key="adminDashBoard"
                    path="/admin/dashboard"
                    component={MyCourseCards}></Route>,
                  <Redirect key="adminDashboardRedirect" to="/student/courses" />
                ]
              : [
                  <Route key="login" path="/login" component={Login}></Route>,
                  <Route key="register" path="/register" component={Register}></Route>,
                  <Redirect key="loginRedirect" to="/login" />
                ]}
          </div>
        </Content>
        <Footer
          style={{
            textAlign: "center",
            position: "fixed",
            bottom: "0px",
            width: "100%"
          }}>
          Ant Design ©2018 Created by Ant UED
        </Footer>
      </Layout>
    </Router>
  );
};

const mapStateToProps = state => {
  return {
    auth: state.auth
  };
};

export default connect(mapStateToProps, { logout })(withRouter(App));
