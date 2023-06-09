<script setup>
const props = defineProps(["product"]);

const attributes = Object.keys(props.product.specification);
</script>

<template>
  <v-table class="table">
    <tbody>
      <tr v-for="(attribute, index) in attributes" :key="index">
        <td class="f">{{ attribute }}</td>
        <td>
          {{ props.product.specification[attribute].value }}
          {{ props.product.specification[attribute].unit || "" }}
        </td>
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
