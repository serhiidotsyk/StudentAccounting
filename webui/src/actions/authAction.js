import axios from "axios";
import { SET_CURRENT_USER } from "./types";
import jwt from "jsonwebtoken";
import setAuthorizationToken from "../utils/setAuthorizationToken";
import { API_LOGIN, API_REGISTER } from "../config";
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
      loginByJWT(res.data.access_token, dispatch);
    });
  };
}

const loginByJWT = (token, dispatch) => {
  console.log();
  var user = jwt.decode(token);
  localStorage.setItem("jwtToken", token);
  setAuthorizationToken(token);
  dispatch(setCurrentUser(user));
};

const openNotification = (code) => {
  notification.info({
    message: `Notification ${"example"}`,
    description:
      'This is the content of the notification. This is the content of the notification. This is the content of the notification.',
  });
}

export function register(data) {
  console.log("--data--", data);
  return dispatch => {
    return axios.post(API_REGISTER, data).then(res => {
      openNotification();
      console.log(res);
    });
  };
}
