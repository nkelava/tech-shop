<script setup>
const props = defineProps(["order"]);

const category = "laptops";
const subcategory = "notebooks";
</script>

<template>
  <v-container class="main pa-10 rounded-lg">
    <v-row class="order-info mb-3">
      <v-col>
        <v-row> Date Placed </v-row>
        <v-row> {{ props.order.datePlaced }} </v-row>
      </v-col>
      <v-col>
        <v-row> Order number </v-row>
        <v-row> {{ props.order.orderNumber }} </v-row>
      </v-col>
      <v-col>
        <v-row> Total Amount </v-row>
        <v-row> {{ props.order.totalAmount }}$</v-row>
      </v-col>
    </v-row>
    <v-row>
      <v-table class="w-100 transparent">
        <thead>
          <tr>
            <th class="text-left">Product</th>
            <th class="text-left">Price</th>
            <th class="text-left">Status</th>
            <th class="text-left">Info</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="product in props.order.products" :key="product.id">
            <td class="d-flex align-center py-10">
              <img :src="product.img" class="mr-4" height="64" width="64" alt="" />
              {{ product.name }}
            </td>
            <td>{{ product.price }}$</td>
            <td>{{ order.status }}</td>
            <td>
              <router-link
                :to="{
                  name: 'product',
                  params: { category: category, subcategory: subcategory, productId: product.id },
                }"
                >View Product</router-link
              >
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
  </v-container>
</template>

<style scoped>
.main {
  background-color: var(--ts-c-primary-soft);
}

.order-info {
  background: var(--ts-c-bg-dark);
  border-radius: 10px;
  padding: 10px 20px;
}

.transparent {
  background: transparent;
  color: var(--ts-c-secondary);
}
</style>
