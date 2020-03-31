import React, { useState, useEffect } from "react";
import { connect } from "react-redux";
import {
  getAllUsers,
  updateStudent,
  deleteStudent,
  deleteStudents
} from "../../../actions/usersTableAction";
import { Table, Input, InputNumber, Popconfirm, Form, Button } from "antd";
import "./AddUsers.css";

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

  const [form] = Form.useForm();
  const [editingId, setEditingId] = useState("");
  const [selectedRowKeys, setSelectedRowKeys] = useState([]);

  
  const hasSelected = selectedRowKeys.length > 0;

  useEffect(() => {
    getAllUsers();
  }, [getAllUsers]);

  const onSelectChange = selectedRowKey => {
    console.log('selectedRowKeys changed: ', selectedRowKey);
    setSelectedRowKeys(selectedRowKey);
    console.log("selected keys", selectedRowKeys);
  };

  const rowSelection = {
    selectedRowKeys,
    onChange: onSelectChange,
  };

  const isEditing = record => record.id === editingId;

  const edit = record => {
    console.log("edit", record);
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
    console.log("deleted students", selectedRowKeys);
    //deleteStudents(selectedRowKeys);
  }

  const save = async record => {
    console.log("----record", record);
    try {
      const row = await form.validateFields();
      const studentToUpdate = Object.assign(record, row);
      console.log("update", studentToUpdate);
      updateStudent(studentToUpdate).then(setEditingId(""));
    } catch (errInfo) {
      console.log("Validate Failed:", errInfo);
    }
  };

  const columns = [
    {
      title: "First Name",
      dataIndex: "firstName",
      editable: true
    },
    {
      title: "Last Name",
      dataIndex: "lastName",
      editable: true
    },
    {
      title: "Age",
      dataIndex: "age",
      editable: true
    },
    {
      title: "Email",
      dataIndex: "email",
      editable: false
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
              <button
                type="button"
                className="link-button"
                href="/admin/dashboard/#">
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
            <button
              key="delete"
              type="button"
              className="link-button"
              disabled={editingId !== ""}
              onClick={() => deleteUser(record.id)}>
              Delete
            </button>
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
      <Search className="search-table" placeholder="Search" enterButton></Search>
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
        rowSelection={rowSelection}
        // pagination={{
        //   onChange: cancel
        // }}
      />
      <Button type="primary" onClick={deleteUsers} disabled={!hasSelected}>Delete selected</Button>
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
