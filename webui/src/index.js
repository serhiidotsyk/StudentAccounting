import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { createBrowserHistory } from "history";
import configureStore from "./store/configureStore";
import * as serviceWorker from "./serviceWorker";
import setAuthorizationToken from "./utils/setAuthorizationToken";
import createAxiosResponseInterceptor from "./utils/axiosInterceptor";
import { setCurrentUser } from "./actions/authAction";
import jwt from "jsonwebtoken";
import App from "./App";

import "./index.css";
import "antd/dist/antd.css";
import { Router } from "react-router-dom";

// Create browser history to use in the Redux store
const history = createBrowserHistory();

// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history, initialState);

if (localStorage.jwtToken) {
  let token = localStorage.jwtToken;
  let user = jwt.decode(token);
  setAuthorizationToken(token);
  store.dispatch(setCurrentUser(user));
}

createAxiosResponseInterceptor();

const rootElement = document.getElementById("root");

ReactDOM.render(
  <Provider store={store}>
    <Router history={history}>
      <App />
    </Router>
  </Provider>,
  rootElement
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
