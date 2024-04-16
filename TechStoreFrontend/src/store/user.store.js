import axios, { axiosPrivate } from "@/api/axios";
import { defineStore } from "pinia";

export const useUserStore = defineStore("user", {
  state: () => ({
    user: null,
    error: null,
  }),
  getters: {
    isLoggedIn: (state) => !!state.user,
  },
  actions: {
    async getUser() {
      if (!this.isLoggedIn) return;

      // const response = await axios.get(`api/v1/user/${this.user.id}`);
      // this.user = { ...response.data.user, accessToken: this.user.token };
    },

    async loginUser(email, password) {
      await axios
        .post("/auth/login", { email, password })
        .then((response) => (this.user = response.data))
        .catch((error) => (this.error = error.reponse ? error.response.data : error));
    },

    async registerUser(email, password, confirmPassword) {
      await axios
        .post("/auth/register", {
          email,
          password,
          confirmPassword,
          FirstName: "Test",
          LastName: "Test",
        })
        .then((response) => (this.user = response.data))
        .catch((error) => (this.error = error.reponse ? error.response.data : error));
    },

    async logoutUser() {
      await axiosPrivate
        .get("/auth/logout")
        .catch((error) => (this.error = error.reponse ? error.response.data : error));

      this.clearStore();
    },

    async deleteUser() {
      // await axios.delete(`api/v1/user/${this.user.id}`);
    },

    async clearStore() {
      this.$reset();
    },
  },
  persist: true,
});
