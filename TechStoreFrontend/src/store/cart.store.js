import { defineStore } from "pinia";

export const useCartStore = defineStore("cart", {
  state: () => ({
    items: [],
  }),
  getters: {
    totalPrice: (state) => (promoCodeDiscount) => {
      const sum = state.items.reduce(
        (total, product) => (total += product.price * product.quantity),
        0
      );
      const discount = sum * (promoCodeDiscount / 100);
      return (sum - discount).toFixed(2);
    },
    itemCount: (state) => state.items.length,
    productQuantity: (state) => (id) => {
      const itemIndex = state.items.findIndex((item) => item.id === id);

      if (itemIndex >= 0) {
        return state.items[itemIndex].quantity;
      }
    },
    productTotal: (state) => (id) => {
      const itemIndex = state.items.findIndex((item) => item.id === id);

      if (itemIndex >= 0) {
        return state.items[itemIndex].quantity * state.items[itemIndex].price;
      }
    },
  },
  actions: {
    addItem(item) {
      const itemExists = this.items.find((i) => i.id === item.id);

      if (itemExists) return;

      item = { ...item, quantity: 1 };

      this.items.push(item);
      this.persistData();
    },
    incrementQuantity(id) {
      const itemIndex = this.items.findIndex((item) => item.id === id);

      if (itemIndex >= 0) {
        this.items[itemIndex].quantity += 1;
        this.persistData();
      }
    },
    decrementQuantity(id) {
      const itemIndex = this.items.findIndex((item) => item.id === id);

      if (itemIndex >= 0) {
        if (this.items[itemIndex].quantity > 1) {
          this.items[itemIndex].quantity -= 1;
          this.persistData();
        }
      }
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
  persist: true,
});
