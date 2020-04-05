import axios from "axios";
import { SET_CURRENT_USER } from "./types";
import jwt from "jsonwebtoken";
import setAuthorizationToken from "../utils/setAuthorizationToken";
import { API_LOGIN, API_SOCIAL_LOGIN, API_REGISTER } from "../config";
import { notification } from "antd";

export function setCurrentUser(user) {
  return {
    type: SET_CURRENT_USER,
    user
  };
}

export function logout() {
  return dispatch => {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("refreshToken");
    setAuthorizationToken(false);
    dispatch(setCurrentUser({}));
  };
}

export function login(data) {
  console.log(data);
  return dispatch => {
    return axios.post(API_LOGIN, data).then(res => {
      console.log(res);
      openNotification(res);
      loginByJWT(res.data, dispatch);
    })
    .catch(function (error){
      console.log(error)
      throw error;
    });
  };
}

export function socialLogin(data) {
  return dispatch => {
    return axios.post(API_SOCIAL_LOGIN, data).then(res => {
      console.log(res);
      openNotification(res);
      loginByJWT(res.data, dispatch);
    })
    .catch(error => {
      openNotification(error.response.data.title, error.response.data.errors);
      throw error;
    });
  };
}

const loginByJWT = (token, dispatch) => {
  var user = jwt.decode(token.access_token);
  localStorage.setItem("refreshToken", token.refresh_token)
  localStorage.setItem("jwtToken", token.access_token);
  setAuthorizationToken(token.access_token);
  dispatch(setCurrentUser(user));
};

const openNotification = (title, errors) => {
  notification.error({
    message: `${title}`,
  });
}

export function register(data) {
  console.log("--data--", data);
  return dispatch => {
    return axios.post(API_REGISTER, data).then(res => {
      openNotification();
      console.log(res);
    })
    .catch(error => {
      openNotification(error.response.data.title);
      throw error;
    });
  };
}
