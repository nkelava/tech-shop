<script setup>
import { toRefs } from "vue";

const props = defineProps(["product"]);
const { product } = toRefs(props);
</script>

<template>
  <v-table class="table">
    <tbody>
      <tr v-for="(productAttribute, index) in product.productAttributes" :key="index">
        <td>{{ productAttribute.attribute.name }}</td>
        <td>{{ productAttribute.attributeValue.value }}</td>
      </tr>
    </tbody>
  </v-table>
</template>

<style scoped>
.table {
  background-color: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
}
</style>
