import React, { useEffect } from "react";
import "devextreme/data/odata/store";
import DataGrid, {
  Column,
  Toolbar,
  LoadPanel,
} from "devextreme-react/data-grid";
import { Item } from "devextreme-react/form";
import { useDispatch, useSelector } from "react-redux";
import { Button } from "devextreme-react";
import { getSubscriptionsByUser } from "../../redux/subscriptionReducer";

const UserSubscriptionsList = () => {
  const dispatch = useDispatch();
  const userSubs = useSelector((state) => state.subscription.userSubs);

  useEffect(() => {
    refreshUserSubsDG();
  }, []);

  const refreshUserSubsDG = () => {
    dispatch(getSubscriptionsByUser());
  };
  return (
    <div className="userSubscriptions-Container" style={{ marginTop: "2rem" }}>
      <DataGrid
        id="permission-datagrid"
        className={"dx-card wide-card"}
        dataSource={userSubs}
        showColumnHeaders={true}
        showBorders={true}
        height={"100%"}
        columnAutoWidth={true}
        columnHidingEnabled={true}
      >
        <LoadPanel enabled={true} />
        <Column dataField="id" visible={false} />
        <Column dataField="userId" visible={false} />
        <Column dataField="organizationId" visible={false} />
        <Column dataField="username" visible={false} />
        <Column dataField="title" />
        <Column dataField="canList" />
        <Column dataField="canAdd" />
        <Column dataField="canEdit" />
        <Column dataField="canDelete" />
        <Toolbar>
          <Item location="before">
            <div className="organization">
              <div className="title">Subscriptions</div>
            </div>
          </Item>
          <Item name="searchPanel" location="before" />
          <Item location="after">
            <Button icon="refresh" onClick={refreshUserSubsDG} />
          </Item>
        </Toolbar>
      </DataGrid>
    </div>
  );
};

export default React.memo(UserSubscriptionsList);
