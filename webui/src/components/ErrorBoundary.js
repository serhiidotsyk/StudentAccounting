import React from "react";
import StackTrace from "stacktrace-js";

function errback(err) {
  console.log(err.message);
}
function callback(stackframes) {
  var stringifiedStack = stackframes
    .map(function (sf) {
      return sf.toString();
    })
    .join("\n");
    console.log(stringifiedStack);
}

export default class ErrorBoundary extends React.Component {
  constructor(props) {
    super(props);
    this.state = { hasError: false };
    console.log("ctor error boundary");
  }
  static getDerivedStateFromError(error) {
    console.log("get state from error");
    // Update state so the next render will show the fallback UI.
    return { hasError: true };
  }
  componentDidCatch(error, info) {
    console.log("component did catch");
    // You can also log the error to an error reporting service
    StackTrace.fromError(error).then(callback).catch(errback);
    console.log(this.state);
  }
  render() {
    if (this.state.hasError) {
      // You can render any custom fallback UI
      return <h1>Something went wrong.</h1>;
    }
    return this.props.children;
  }
}
