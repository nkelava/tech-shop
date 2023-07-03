import { defineStore } from "pinia";

export const useWishlistStore = defineStore("wishlist", {
  state: () => ({
    items: [],
  }),
  getters: {
    itemCount: (state) => state.items.length,
  },
  actions: {
    addItem(item) {
      this.items.push(item);
      this.persistData();
    },
    removeItem(id) {
      this.items = this.items.filter((item) => item.id != id);
      this.persistData();
    },
    persistData() {
      localStorage.setItem("wishlist", JSON.stringify(this.items));
    },
    loadData() {
      const data = localStorage.getItem("wishlist");
      if (data) {
        this.items = JSON.parse(data);
      }
    },
  },
});
