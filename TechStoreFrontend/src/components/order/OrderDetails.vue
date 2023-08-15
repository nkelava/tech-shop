<script setup>
const props = defineProps(["order"]);
const category = "laptops";
const subcategory = "notebooks";
</script>

<template>
  <v-container class="account-orders rounded-lg">
    <v-row class="order__info">
      <v-col class="pb-0 pb-sm-3" cols="12" sm="4">
        <h4>Date Placed</h4>
        <p>{{ props.order.datePlaced }}</p>
      </v-col>
      <v-col class="pt-0 pt-sm-3">
        <h4>Order number</h4>
        <p>{{ props.order.orderNumber }}</p>
      </v-col>
      <v-col class="pt-0 pt-sm-3">
        <h4>Total Amount</h4>
        <p>{{ props.order.totalAmount }}$</p>
      </v-col>
    </v-row>
    <v-row class="test">
      <v-table class="order__table transparent">
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
              <img :src="product.img" height="64" width="64" alt="" :title="product.name" />
            </td>
            <td>{{ product.price }}$</td>
            <td>{{ order.status }}</td>
            <td>
              <router-link
                :to="{
                  name: 'product',
                  params: { category: category, subcategory: subcategory, productId: product.id },
                }"
                class="link"
              >
                View More
              </router-link>
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
  </v-container>
</template>

<style scoped>
.account-orders {
  background-color: var(--ts-c-primary-soft);
}

.order__info {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  gap: 10px;
  margin-top: 10px;
  font-size: 14px;
  background: var(--ts-c-bg-dark);
  border-radius: 10px;
}

.order__table {
  width: 100%;
  font-size: 14px;
}

::v-deep table {
  padding-bottom: 20px;
}

.link {
  color: var(--ts-c-ternary);
}

.transparent {
  background: transparent;
  color: var(--ts-c-secondary);
}
</style>
