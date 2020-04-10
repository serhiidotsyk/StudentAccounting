import axios from "axios";
import { SET_CURRENT_USER } from "./types";
import jwt from "jsonwebtoken";
import setAuthorizationToken from "../utils/setAuthorizationToken";
import { API_LOGIN, API_SOCIAL_LOGIN, API_REGISTER } from "../config";
import { trackPromise } from "react-promise-tracker";

export function setCurrentUser(user) {
  return {
    type: SET_CURRENT_USER,
    user,
  };
}

export function logout() {
  return (dispatch) => {
    localStorage.removeItem("jwtToken");
    localStorage.removeItem("refreshToken");
    setAuthorizationToken(false);
    dispatch(setCurrentUser({}));
  };
}

export function login(data) {
  console.log(data);
  return (dispatch) => {
    return trackPromise(
      axios.post(API_LOGIN, data).then((res) => {
        loginByJWT(res.data, dispatch);
      })
    );
  };
}

export function socialLogin(data) {
  return (dispatch) => {
    return trackPromise(
      axios.post(API_SOCIAL_LOGIN, data).then((res) => {
        console.log(res);
        loginByJWT(res.data, dispatch);
      })
    );
  };
}

const loginByJWT = (token, dispatch) => {
  var user = jwt.decode(token.access_token);
  localStorage.setItem("refreshToken", token.refresh_token);
  localStorage.setItem("jwtToken", token.access_token);
  setAuthorizationToken(token.access_token);
  dispatch(setCurrentUser(user));
};

export function register(data) {
  console.log("--data--", data);
  return (dispatch) => {
    return trackPromise(
      axios.post(API_REGISTER, data).then((res) => {
        console.log(res);
      })
    );
  };
}
