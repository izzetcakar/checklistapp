import React, { useEffect, useRef } from "react";
import "devextreme/data/odata/store";
import DataGrid, {
  Column,
  Editing,
  Popup,
  Paging,
  Form,
  Pager,
  Toolbar,
  Lookup,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item, RequiredRule } from "devextreme-react/form";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "devextreme-react";
import {
  createPermission,
  removePermission,
  updatePermission,
  getPermissionsByUser,
} from "../../redux/permissionReducer";
import DataSource from "devextreme/data/data_source";
import { notifyError } from "../../functions/notifyError";
import { getAllOrganizations } from "../../redux/organizationReducer";
import UserSubscriptionsList from "../subscription/UserSubscriptionsList";

const UserPermission = () => {
  const dispatch = useDispatch();
  const selectedItemKeys = useRef([]);
  const permissions = useSelector((state) => state.permission.permissions);
  const orgData = useSelector((state) => state.organization.organizations);
  const allowedPageSizes = [5, 10, 20, 50, 100];

  useEffect(() => {
    refreshPermissionReqDG();
  }, []);

  const permissionDataSource = new DataSource({
    store: {
      type: "array",
      data: JSON.parse(JSON.stringify(permissions)),
      key: "id",
    },
  });
  const selectionChanged = (data) => {
    selectedItemKeys.current = data.selectedRowKeys;
  };
  const onRowUpdated = async (e) => {
    await dispatch(updatePermission({ ...e.oldData, ...e.newData }));
    refreshPermissionReqDG();
  };
  const onRowInserted = async (e) => {
    let { id, ...rest } = e.data;
    let res = await dispatch(createPermission(rest));
    notifyError(res);
    refreshPermissionReqDG();
  };
  const onRowRemoved = async (e) => {
    await dispatch(removePermission(e.data.id));
    refreshPermissionReqDG();
  };
  const refreshPermissionReqDG = () => {
    dispatch(getPermissionsByUser());
    dispatch(getAllOrganizations());
  };
  const allowEditing = (e) => {
    return e.row.data.status === "Waiting";
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
            onSelectionChanged={selectionChanged}
            onRowInserted={onRowInserted}
            onRowUpdated={onRowUpdated}
            onRowRemoved={onRowRemoved}
          >
            <LoadPanel enabled={true} />
            <Editing
              mode="popup"
              allowUpdating={allowEditing}
              allowAdding={true}
              allowDeleting={true}
              labelLocation="left"
            >
              <Popup
                title="Permission Info"
                showTitle={true}
                width="auto"
                height="auto"
              />
              <Form
                labelLocation="left"
                labelMode="outside"
                alignItemLabels={true}
                alignItemLabelsInAllGroups={true}
              >
                <Item itemType="group" colCount={2} colSpan={2}>
                  <Item
                    dataField="organizationId"
                    caption="Organization"
                    colSpan={2}
                  >
                    <RequiredRule message="Organization is required" />
                  </Item>
                  <Item dataField="canList" />
                  <Item dataField="canAdd" />
                  <Item dataField="canDelete" />
                  <Item dataField="canEdit" />
                </Item>
              </Form>
            </Editing>
            <Paging defaultPageSize={10} />
            <Pager
              showPageSizeSelector={true}
              showInfo={true}
              allowedPageSizes={allowedPageSizes}
            />
            <Column dataField="organizationId" caption="Organization">
              <Lookup dataSource={orgData} displayExpr="title" valueExpr="id" />
            </Column>
            <Column visible={false} dataField="title" caption="Organization" />
            <Column dataField="canList" caption="List" dataType="boolean" />
            <Column dataField="canAdd" caption="Add" dataType="boolean" />
            <Column dataField="canDelete" caption="Delete" dataType="boolean" />
            <Column dataField="canEdit" caption="Edit" dataType="boolean" />
            <Column dataField="status" />
            <Toolbar>
              <Item location="before">
                <div className="organization">
                  <div className="title">Requests</div>
                </div>
              </Item>
              <Item name="addRowButton" />
              <Item location="after">
                <Button icon="refresh" onClick={refreshPermissionReqDG} />
              </Item>
            </Toolbar>
          </DataGrid>
          <UserSubscriptionsList />
        </div>
      </div>
    </React.Fragment>
  );
};

export default UserPermission;
