<script setup>
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "../../../constants/messages/delete";

const props = defineProps({
  categories: {
    type: Array,
    default: () => [],
  },
});
const emit = defineEmits(["reload"]);
const toast = useToast();

async function deleteCategory(categoryId) {
  await axiosPrivate
    .delete(`/categories/${categoryId}`)
    .then((resp) => {
      toast.success(ITEM_DELETE_SUCCESS);
      return resp.data;
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  emit("reload");
}
</script>
<template>
  <v-container class="categories__container">
    <v-row>
      <h2>Category List</h2>
    </v-row>
    <v-row>
      <v-table class="categories__table">
        <thead>
          <tr>
            <th class="text-left">Name</th>
            <th class="text-left">Slug</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(category, id) in props.categories" :key="id">
            <td :title="category.name">{{ category?.name }}</td>
            <td :title="category.slug">{{ category?.slug }}</td>
            <td class="d-flex align-center">
              <v-btn
                color="red"
                icon="mdi-delete"
                size="32"
                title="Delete"
                alt="Delete"
                @click="deleteCategory(category.id)"
              />
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
  </v-container>
</template>

<style scoped>
.categories__table {
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
