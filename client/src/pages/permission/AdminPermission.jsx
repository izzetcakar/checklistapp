import React, { useEffect } from "react";
import "devextreme/data/odata/store";
import DataGrid, {
  Column,
  Paging,
  Pager,
  Toolbar,
  Editing,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item } from "devextreme-react/form";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "devextreme-react";
import {
  removePermission,
  getAllPermissions,
} from "../../redux/permissionReducer";
import DataSource from "devextreme/data/data_source";
import PermissionEditCell from "../permission/PermissionEditCell";

const AdminPermission = () => {
  const dispatch = useDispatch();
  const permissions = useSelector((state) => state.permission.permissions);
  const allowedPageSizes = [5, 10, 20, 50, 100];

  useEffect(() => {
    refreshDatagrid();
  }, []);

  const permissionDataSource = new DataSource({
    store: {
      type: "array",
      data: JSON.parse(JSON.stringify(permissions)),
      key: "id",
    },
  });

  const onRowRemoving = async (e) => {
    await dispatch(removePermission(e.data.id));
    refreshDatagrid();
  };
  const refreshDatagrid = () => {
    dispatch(getAllPermissions());
  };

  return (
    <React.Fragment>
      <h2 className={"content-block"}>Permissions</h2>
      <div className={"content-block"}>
        <div
          className={"dx-card responsive-paddings"}
          style={{ height: "100vh" }}
        >
          <DataGrid
            id="permission-request-datagrid"
            className={"dx-card wide-card"}
            dataSource={permissionDataSource}
            showColumnHeaders={true}
            showBorders={true}
            height={"100%"}
            columnAutoWidth={false}
            columnHidingEnabled={false}
            onRowRemoving={onRowRemoving}
          >
            <LoadPanel enabled={true} />
            <Editing mode="popup" allowDeleting={true} labelLocation="left" />
            <Paging defaultPageSize={10} />
            <Pager
              showPageSizeSelector={true}
              showInfo={true}
              allowedPageSizes={allowedPageSizes}
            />
            <Column dataField="username" caption="User" />
            <Column dataField="title" caption="Organization" />
            <Column dataField="canList" caption="List" dataType="boolean" />
            <Column dataField="canAdd" caption="Add" dataType="boolean" />
            <Column dataField="canDelete" caption="Delete" dataType="boolean" />
            <Column dataField="canEdit" caption="Edit" dataType="boolean" />
            <Column dataField="status" />
            <Column cellComponent={PermissionEditCell} />
            <Toolbar>
              <Item location="before">
                <div className="organization">
                  <div className="title">Requests</div>
                </div>
              </Item>
              <Item name="addRowButton" />
              <Item location="after">
                <Button icon="refresh" onClick={refreshDatagrid} />
              </Item>
            </Toolbar>
          </DataGrid>
        </div>
      </div>
    </React.Fragment>
  );
};

export default AdminPermission;
