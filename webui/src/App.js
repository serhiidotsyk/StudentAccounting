import React, { Component } from "react";
import "./App.css";
import { Route, withRouter } from "react-router";
import { BrowserRouter as Router, Link } from "react-router-dom";

import Home from "./components/home/Home";
import Login from "./components/auth/login/Login";
import Register from "./components/auth/register/Register";
import { Layout, Menu } from "antd";

import { connect } from "react-redux";
import { logout, login } from "./actions/authAction";
import Profile from "./components/profile/Profile";
import logo from "./logo.svg";

const { Header, Content, Footer } = Layout;

class App extends Component {
  logout() {
    this.props.logout();
  }
  render() {
    const logoutLink = (
      <Menu.Item key="logout" onClick={this.logout.bind(this)}>
        <Link to="/login">Logout</Link>
      </Menu.Item>
    );

    const loginLinks = (
      <Menu.Item key="login">
        <Link to="/login">Login</Link>
      </Menu.Item>
    );

    const { isAuthenticated } = this.props.auth;

    return (
      <Router>
        <Layout className="layout">
          <Header>
            <div className="logo">
              <a href="/">
                <img src={logo} alt="logo" />
              </a>
            </div>
            <Menu
              theme="dark"
              mode="horizontal"
              defaultSelectedKeys={[this.props.location.pathname.substr(1)]}
              style={{ lineHeight: "64px" }}>
              {!isAuthenticated ? (
                <Menu.Item key="register">
                  <Link to="/register">Register</Link>
                </Menu.Item>
              ) : (
                ""
              )}
              {isAuthenticated ? (
                <Menu.Item key="profile">
                  <Link to="/profile">Profile</Link>
                </Menu.Item>
              ) : (
                ""
              )}
              {isAuthenticated ? logoutLink : loginLinks}
            </Menu>
          </Header>
          <Content style={{ padding: "0 50px" }}>
            <div align="middle" className="site-layout-content">
              <Route exact path="/" component={Home}></Route>
              <Route path="/login" component={Login}></Route>
              <Route path="/register" component={Register}></Route>
              <Route path="/profile" component={Profile}></Route>
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
  }
}

const mapStateToProps = state => {
  return {
    auth: state.auth
  };
};

export default connect(mapStateToProps, { logout })(withRouter(App));
