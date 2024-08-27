<script setup>
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";
import { axiosPublic } from "@/api/axios";
import OrderConfirmationImage from "@/assets/images/test/order/order_confirmation.jpg";

const route = useRoute();
const router = useRouter();
const session_id = ref(route.query.session_id);

onMounted(async () => {
  // Get session id from URL
  await axiosPublic
    .get(`/orders/confirm/${session_id?.value}`)
    .then((response) => console.log(response.data))
    .catch((error) => console.log(error));
  // Make a POST request to API to finish the order (clear cart, update order status, etc.)
});
</script>

<template>
  <v-card class="order__container text-center ma-auto my-16 pa-16 pb-4" width="600">
    <div class="order__content">
      <v-img class="order__image" :src="OrderConfirmationImage" />
      <h1>Thank you for ordering!</h1>
      <p>
        <span
          >Your order has been placed successfully. If you have any questions, please email</span
        >
        <a href="mailto:techplanet.team@gmail.com"> techplanet.team@gmail.com</a>
        <span>.</span>
      </p>
    </div>
    <div class="order__footer">
      <v-btn class="view-btn" @click="router.push('/user')">View Order</v-btn>
      <v-btn class="continue-btn" @click="router.push('/')">Continue Shopping</v-btn>
    </div>
  </v-card>
</template>

<style scoped>
.order__container {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  background-color: var(--ts-c-bg-light);
}

.order__content h1 {
  color: var(--ts-c-ternary);
}

.order__content p :not(a) {
  opacity: 0.4;
}

.order__content p a {
  color: var(--ts-c-text-dark);
}

.order__image {
  border-radius: 5px;
  margin-bottom: 1rem;
}

.order__footer {
  display: flex;
  justify-content: center;
  gap: 4rem;
}

.view-btn {
  box-shadow: none;
  font-weight: bold;
  color: var(--ts-c-text-dark);
  border: 1px solid var(--ts-c-text-dark);
  background-color: transparent;
  text-transform: uppercase;
  letter-spacing: normal;
}

.continue-btn {
  box-shadow: none;
  font-weight: bold;
  color: var(--ts-c-text-dark);
  background-color: var(--ts-c-ternary);
  text-transform: uppercase;
  letter-spacing: normal;
}
</style>
