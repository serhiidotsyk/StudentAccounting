import React from "react";
import { Table } from "antd";

export function NestedTable(courses) {
  const columns = [
    { title: "Name", dataIndex: "name" },
    { title: "Duration days", dataIndex: "durationDays" },
    { title: "Start Date", dataIndex: "startDate" },
    { title: "End Date", dataIndex: "endDate" }
  ];

  return (
    <Table
      columns={columns}
      dataSource={courses}
      rowKey="id"
      pagination={false}
    />
  );
}
