const user = JSON.parse(localStorage.getItem("user"));
const accessToken = user?.token ? user.token : null;

const defaultOptions = {
  baseURL: import.meta.env.API_BASE_URL,
};

const privateOptions = {
  ...defaultOptions,
  headers: {
    Authorization: `Bearer ${accessToken}`,
  },
  withCredentials: true,
  credentials: "include",
};

export { defaultOptions, privateOptions };
