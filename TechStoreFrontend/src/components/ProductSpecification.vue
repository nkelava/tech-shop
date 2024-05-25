<script setup>
import { toRefs } from "vue";

const props = defineProps(["product"]);
const { product } = toRefs(props);
</script>

<template>
  <div>
    <v-table v-if="product?.productAttributes?.length" class="table">
      <tbody>
        <tr v-for="(productAttribute, index) in product.productAttributes" :key="index">
          <td>{{ productAttribute.attribute.name }}</td>
          <td>{{ productAttribute.attributeValue.value }}</td>
        </tr>
      </tbody>
    </v-table>
    <p v-else>No specification available.</p>
  </div>
</template>

<style scoped>
.table {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}

p {
  padding: 1rem;
  font-size: 1rem;
}
</style>
