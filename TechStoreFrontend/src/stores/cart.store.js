import { defineStore } from "pinia";

export const useCartStore = defineStore("cart", {
  state: () => ({
    items: [],
  }),
  getters: {
    totalPrice: (state) => (promoCodeDiscount) => {
      const sum = state.items.reduce((total, product) => (total += product.price), 0);
      const discount = sum * (promoCodeDiscount / 100);
      return (sum - discount).toFixed(2);
    },
    itemCount: (state) => state.items.length,
  },
  actions: {
    addItem(item) {
      const itemExists = this.items.find((i) => i.id === item.id);

      if (itemExists) return;

      this.items.push(item);
      this.persistData();
    },
    removeItem(id) {
      this.items = this.items.filter((item) => item.id != id);
      this.persistData();
    },
    persistData() {
      localStorage.setItem("cart", JSON.stringify(this.items));
    },
    loadData() {
      const data = localStorage.getItem("cart");
      if (data) {
        this.items = JSON.parse(data);
      }
    },
  },
});
