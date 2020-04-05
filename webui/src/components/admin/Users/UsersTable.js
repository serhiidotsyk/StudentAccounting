import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import {
  getAllUsers,
  updateStudent,
  deleteStudent,
  deleteStudents
} from "../../../actions/usersTableAction";
import {
  Table,
  Input,
  InputNumber,
  Popconfirm,
  Form,
  Button,
  Pagination
} from "antd";
import "./AddUsers.css";

import { NestedTable } from "./NestedCourseTable";

const { Search } = Input;

const EditableCell = ({
  editing,
  dataIndex,
  title,
  inputType,
  children,
  ...restProps
}) => {
  const inputNode = inputType === "number" ? <InputNumber /> : <Input />;

  return (
    <td {...restProps}>
      {editing ? (
        <Form.Item
          name={dataIndex}
          style={{ margin: 0 }}
          rules={[
            {
              required: true,
              message: `Please Input ${title}!`
            }
          ]}>
          {inputNode}
        </Form.Item>
      ) : (
        children
      )}
    </td>
  );
};

const UsersTable = ({
  getAllUsers,
  updateStudent,
  deleteStudent,
  deleteStudents,
  ...props
}) => {
  const { users } = props.allUsers;
  const { count } = props.allUsers;

  const [form] = Form.useForm();

  const [editingId, setEditingId] = useState("");
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [searchString, setSearchString] = useState("");
  const [sortOrder, setSortOrder] = useState("");
  const [sortField, setSortField] = useState("");

  const pageSizeOption = ["5", "10", "15", "20"];

  const hasSelected = selectedRowKeys.length > 0;

  useEffect(() => {
    getAllUsers({ pageNumber, pageSize, searchString, sortOrder, sortField });
  }, [getAllUsers, pageNumber, pageSize, searchString, sortOrder, sortField]);

  useEffect(() => {
    const { length } = users;
    if (length <= 0 && pageNumber > 1) {
      setPageNumber(pageNumber - 1);
    }
  }, [users, pageNumber]);

  const onSearch = (value, event) => {
    setSearchString(value);
    console.log(typeof value);
  };
  const handleSortChange = (pagiantion, filter, sorter) => {
    setSortOrder(sorter.order);
    setSortField(sorter.field);
  };
  const onSelectChange = selectedRowKey => {
    setSelectedRowKeys(selectedRowKey);
  };

  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange
  };

  const isEditing = record => record.id === editingId;

  const edit = record => {
    form.setFieldsValue({ ...record });
    setEditingId(record.id);
  };

  const cancel = () => {
    setEditingId("");
  };

  const deleteUser = userId => {
    deleteStudent(userId);
  };

  const deleteUsers = () => {
    deleteStudents(selectedRowKeys);
  };

  const save = async record => {
    try {
      const row = await form.validateFields();
      const studentToUpdate = Object.assign(record, row);
      updateStudent(studentToUpdate).then(setEditingId(""));
    } catch (errInfo) {
      console.log("Validate Failed:", errInfo);
    }
  };

  const expandedRowRender = (record) => {
    return NestedTable(record.courses);
  };
  const onPaginationChange = (page, _) => {
    cancel();
    setPageNumber(page);
  };
  const onShowSizeChange = (_, size) => {
    setPageSize(size);
  };

  const MyPagination = ({
    total,
    onChange,
    current,
    onShowSizeChange,
    pageSizeOption
  }) => {
    console.log(total, onChange, current);
    return (
      <Pagination
        showSizeChanger
        onShowSizeChange={onShowSizeChange}
        pageSize={pageSize}
        current={current}
        total={total}
        onChange={onChange}
        pageSizeOptions={pageSizeOption}></Pagination>
    );
  };

  const columns = [
    {
      title: "First Name",
      dataIndex: "firstName",
      editable: true,
      sorter: true
    },
    {
      title: "Last Name",
      dataIndex: "lastName",
      editable: true,
      sorter: true
    },
    {
      title: "Age",
      dataIndex: "age",
      editable: true,
      sorter: true
    },
    {
      title: "Email",
      dataIndex: "email",
      editable: false,
      sorter: true
    },
    {
      title: "operation",
      dataIndex: "operation",
      render: (_, record) => {
        const editable = isEditing(record);
        return editable ? (
          <span>
            <button
              type="button"
              className="link-button"
              onClick={() => save(record)}>
              Save
            </button>
            <Popconfirm title="Sure to cancel?" onConfirm={cancel}>
              <button type="button" className="link-button">
                Cancel
              </button>
            </Popconfirm>
          </span>
        ) : (
          [
            <button
              key="edit"
              type="button"
              className="link-button"
              disabled={editingId !== ""}
              onClick={() => edit(record)}>
              Edit
            </button>,
            <Popconfirm
              key="delete"
              title="Sure to delete?"
              onConfirm={() => deleteUser(record.id)}>
              <button
                type="button"
                className="link-button"
                disabled={editingId !== ""}>
                Delete
              </button>
            </Popconfirm>
          ]
        );
      }
    }
  ];

  const mergedColumns = columns.map(col => {
    if (!col.editable) {
      return col;
    }
    return {
      ...col,
      onCell: record => ({
        record,
        inputType: col.dataIndex === "age" ? "number" : "text",
        dataIndex: col.dataIndex,
        title: col.title,
        editing: isEditing(record)
      })
    };
  });

  return (
    <Form form={form} component={false}>
      <Search
        className="search-table"
        placeholder="Search"
        enterButton
        onSearch={onSearch}></Search>
      <Table
        components={{
          body: {
            cell: EditableCell
          }
        }}
        bordered
        dataSource={users}
        columns={mergedColumns}
        rowClassName="editable-row"
        rowKey="id"
        onChange={handleSortChange}
        rowSelection={rowSelection}
        expandable={{
          expandedRowRender,
          rowExpandable: record => record.courses.length > 0
        }}
        pagination={false}
      />
      <MyPagination
        total={count}
        current={pageNumber}
        onChange={onPaginationChange}
        onShowSizeChange={onShowSizeChange}
        pageSizeOption={pageSizeOption}
      />
      <Popconfirm
        disabled={!hasSelected}
        title="Sure to delete?"
        onConfirm={deleteUsers}>
        <Button type="primary" disabled={!hasSelected}>
          Delete selected
        </Button>
      </Popconfirm>
    </Form>
  );
};

const mapStateToProps = state => {
  return {
    allUsers: state.usersTable
  };
};

export default connect(mapStateToProps, {
  getAllUsers,
  deleteStudent,
  deleteStudents,
  updateStudent
})(UsersTable);
