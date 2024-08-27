<script setup>
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();
const sessionId = ref(route.query);

onMounted(() => {
  // Get session id from URL
  console.log("query params:  ", { params: route.query });
  // Make a POST request to API to finish the order (clear cart, update order status, etc.)
});
</script>

<template>
  <h1>Your order has failed!</h1>
  <p>
    We appreciate your business! Please email
    <a href="mailto:techplanet.team@gmail.com">techplanet.team@gmail.com</a>.
  </p>
</template>
