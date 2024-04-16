const userLocalStorage = JSON.parse(localStorage.getItem("user"));
const accessToken = userLocalStorage && userLocalStorage.user ? userLocalStorage.user.token : null;

const defaultOptions = {
  baseURL: import.meta.env.VITE_API_BASE_URL,
};

const privateOptions = {
  ...defaultOptions,
  headers: {
    "Content-type": "application/json",
    Authorization: `Bearer ${accessToken}`,
  },
  withCredentials: true,
  credentials: "include",
};

export { defaultOptions, privateOptions };
