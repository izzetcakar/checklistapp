import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { request } from "../server/api";
const requestUrl = "ListItem/";

export const getAllListItems = createAsyncThunk(
  "listItems/getAll",
  async () => {
    const response = await request({
      requestUrl: requestUrl,
      apiType: "getAll",
    });
    return response.data;
  }
);
export const getListItemById = createAsyncThunk(
  "listItems/getById",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const getListItemsByChkId = createAsyncThunk(
  "listItems/getAllByChkId",
  async (Id) => {
    const response = await request({
      requestUrl: requestUrl + `GetByChkId/${Id}`,
      apiType: "get",
    });
    return response.data;
  }
);
export const createListItem = createAsyncThunk(
  "listItems/create",
  async (listItem, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: listItem,
      apiType: "post",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const updateListItem = createAsyncThunk(
  "listItems/update",
  async (listItem, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl,
      queryData: listItem,
      apiType: "put",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const removeListItem = createAsyncThunk(
  "listItems/remove",
  async (Id, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: requestUrl + `${Id}`,
      apiType: "delete",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);
export const removeListItemByIds = createAsyncThunk(
  "listItems/removeByIds",
  async (Ids, { rejectWithValue, fulfillWithValue }) => {
    const response = await request({
      requestUrl: "ListItem",
      queryData: Ids,
      apiType: "deleteAll",
    });
    if (!response.success) {
      return rejectWithValue(response.message);
    } else {
      return fulfillWithValue(null);
    }
  }
);

const listItemSlice = createSlice({
  name: "listItem",
  initialState: {
    listItems: [],
    listItem: {},
    status: "idle",
    error: null,
  },
  reducers: {
    clearListItem: (state, action) => {
      state.listItem = {};
    },
    setListItems: (state, action) => {
      state.listItems = action.payload;
    },
    setListItem: (state, action) => {
      state.listItem = action.payload;
    },
  },
  extraReducers(builder) {
    builder.addCase(getAllListItems.fulfilled, (state, action) => {
      state.listItems = action.payload;
    });
    builder.addCase(getListItemsByChkId.fulfilled, (state, action) => {
      state.listItems = action.payload;
    });
    builder.addCase(getListItemById.fulfilled, (state, action) => {
      state.listItem = action.payload;
    });
  },
});

export const { clearListItem, setListItem, setListItems } =
  listItemSlice.actions;
export default listItemSlice.reducer;
