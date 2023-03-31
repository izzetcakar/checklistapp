import { request } from "../server/api";
import defaultUser from "../utils/default-user";

export async function signIn(userName, password) {
  try {
    let result = await request({
      requestUrl: "User/ogin",
      queryData: { userName: userName, password: password },
      apiType: "post",
    });
    //await localStorage.setItem("token", JSON.stringify(result.data));
    let response = await request({
      requestUrl: "User/getWithToken",
      apiType: "get",
    });

    return {
      isOk: true,
      data: defaultUser,
    };
  } catch {
    return {
      isOk: false,
      message: "Authentication failed",
    };
  }
}

export async function getUser() {
  try {
    let result = await request({
      requestUrl: "User/getWithToken",
      apiType: "get",
    });
    return {
      isOk: true,
      data: result.data,
    };
  } catch {
    return {
      isOk: false,
    };
  }
}

export async function createAccount(formData) {
  try {
    const result = await request({
      requestUrl: "User/register",
      queryData: formData,
      apiType: "post",
    });
    localStorage.setItem("token", JSON.stringify(result.data));
    return true;
  } catch {
    return false;
  }
}

export async function changePassword(email, recoveryCode) {
  try {
    // Send request
    console.log(email, recoveryCode);

    return {
      isOk: true,
    };
  } catch {
    return {
      isOk: false,
      message: "Failed to change password",
    };
  }
}

export async function resetPassword(email) {
  try {
    // Send request
    console.log(email);

    return {
      isOk: true,
    };
  } catch {
    return {
      isOk: false,
      message: "Failed to reset password",
    };
  }
}
