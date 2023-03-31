import React, { useLayoutEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import BaseOptionDataGrid from "./BaseOptionDataGrid";
import "./baseOption.scss";
import { getAllBaseOptions } from "../../redux/baseOptionReducer";

const BaseOption = () => {
  const dispatch = useDispatch();
  const categories = useSelector((state) => state.baseOption.categories);
  const segments = useSelector((state) => state.baseOption.segments);
  const controlLists = useSelector((state) => state.baseOption.controlLists);
  const consepts = useSelector((state) => state.baseOption.consepts);
  const contents = useSelector((state) => state.baseOption.contents);

  const dataMap = [
    {
      data: consepts,
      type: "Consept",
    },
    {
      data: categories,
      type: "Category",
    },
    {
      data: segments,
      type: "Segment",
    },
    {
      data: controlLists,
      type: "ControlList",
    },
    {
      data: contents,
      type: "Content",
    },
  ];

  useLayoutEffect(() => {
    dispatch(getAllBaseOptions());
  }, []);

  return (
    <React.Fragment>
      <h2 className={"content-block"}>Base Options</h2>
      <div className={"content-block"}>
        <div
          className={"dx-card responsive-paddings"}
          style={{ height: "inherit" }}
        >
          <div className="baseOption-container">
            {dataMap.map((item, index) => (
              <div className="dataGrid" key={index}>
                <BaseOptionDataGrid
                  optionData={item.data}
                  optionType={item.type}
                />
              </div>
            ))}
          </div>
        </div>
      </div>
    </React.Fragment>
  );
};

export default BaseOption;
