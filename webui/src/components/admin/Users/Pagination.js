import React from "react";
import { Pagination } from "antd";

function onShowSizeChange(current, pageSize){
  console.log("current", current);
  console.log("page size", pageSize);
}

export function MyPagination({total, onChange, current}) {
  console.log(total, onChange, current, onShowSizeChange);
  return (
    <Pagination
      showSizeChanger
      onShowSizeChange={onShowSizeChange}
      defaultCurrent={current}
      total={total}
      onChange={onChange}
    />
  );
}
