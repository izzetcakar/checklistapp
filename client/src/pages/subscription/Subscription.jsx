import React, { useEffect } from "react";
import "devextreme/data/odata/store";
import DataGrid, {
  Column,
  Editing,
  Paging,
  Pager,
  Toolbar,
  MasterDetail,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item } from "devextreme-react/form";
import { useDispatch, useSelector } from "react-redux";
import DataSource from "devextreme/data/data_source";
import { Button } from "devextreme-react";
import {
  getAllSubscriptions,
  removeSubByUserId,
} from "../../redux/subscriptionReducer";
import SubscriptionTemplate from "./SubscriptionTemplate";
import { getAllUsers } from "../../redux/userReducer";

const Subscription = () => {
  const dispatch = useDispatch();
  const users = useSelector((state) => state.user.users);
  const user = useSelector((state) => state.user.user);
  const allowedPageSizes = [5, 10, 20, 50, 100];

  useEffect(() => {
    dispatch(getAllUsers());
  }, []);

  const subDataSource = new DataSource({
    store: {
      type: "array",
      data: JSON.parse(JSON.stringify(users)),
      key: "id",
    },
  });
  const onRowRemoving = async (e) => {
    await dispatch(removeSubByUserId(e.data.id));
    dispatch(getAllSubscriptions());
  };
  const refreshDataGrid = () => {
    dispatch(getAllUsers());
    dispatch(getAllSubscriptions());
  };

  return (
    <React.Fragment>
      {user.isAdmin ? (
        <>
          <h2 className={"content-block"}>Subscriptions</h2>
          <div className={"content-block"}>
            <div
              className={"dx-card responsive-paddings"}
              style={{ height: "100vh" }}
            >
              <DataGrid
                id="sub-container"
                className={"dx-card wide-card"}
                dataSource={subDataSource}
                showColumnHeaders={false}
                showBorders={true}
                focusedRowEnabled={true}
                height={"100%"}
                columnAutoWidth={true}
                columnHidingEnabled={true}
                onRowRemoving={onRowRemoving}
              >
                <LoadPanel enabled={true} />
                <Editing mode="popup" allowDeleting={true}></Editing>
                <Paging defaultPageSize={10} />
                <Pager
                  showPageSizeSelector={true}
                  showInfo={true}
                  allowedPageSizes={allowedPageSizes}
                />
                <Column dataField="userName" dataType="string" />
                <Toolbar>
                  <Item location="before">
                    <div className="organization">
                      <div className="title">Users</div>
                    </div>
                  </Item>
                  <Item location="after">
                    <Button icon="refresh" onClick={refreshDataGrid} />
                  </Item>
                </Toolbar>
                <MasterDetail enabled={true} component={SubscriptionTemplate} />
              </DataGrid>
            </div>
          </div>
        </>
      ) : null}
    </React.Fragment>
  );
};

export default Subscription;
