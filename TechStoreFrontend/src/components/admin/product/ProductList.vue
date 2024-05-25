<script setup>
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "../../../constants/messages/delete";

const props = defineProps({
  products: {
    type: Array,
    default: () => [],
  },
});
const emit = defineEmits(["reload"]);
const toast = useToast();

async function deleteProduct(productId) {
  await axiosPrivate
    .delete(`/products/${productId}`)
    .then((resp) => {
      if (resp.status === 200) {
        toast.success(ITEM_DELETE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  emit("reload");
}
// TODO: Add pagination (10 items per page)
</script>
<template>
  <v-container class="dashboard__container">
    <v-row>
      <h2>Product List</h2>
    </v-row>
    <v-row>
      <v-table class="dashboard__table">
        <thead>
          <tr>
            <th class="text-left">Name</th>
            <th class="text-left">Price</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(product, id) in props?.products" :key="id">
            <td :title="product?.name">{{ product?.name }}</td>
            <td>${{ product?.price }}</td>
            <td class="d-flex align-center">
              <v-btn
                color="red"
                icon="mdi-delete"
                size="32"
                title="Delete"
                alt="Delete"
                @click="deleteProduct(product?.id)"
              />
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
  </v-container>
</template>

<style scoped>
.dashboard__table {
  width: 100%;
  margin-top: 1rem;
  color: var(--ts-c-text-light);
  background: transparent;
  border: 1px solid var(--ts-c-primary-dark);
  border-radius: 10px;
}

thead {
  background: var(--ts-c-primary-dark);
}

tr {
  vertical-align: center;
}

th {
  color: var(--ts-c-text-light) !important;
}

td {
  max-width: 500px !important;
  overflow: hidden;
  text-overflow: ellipsis;
}
</style>
