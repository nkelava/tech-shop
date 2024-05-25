<script setup>
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "../../../constants/messages/delete";

const props = defineProps({
  subcategories: {
    type: Array,
    default: () => [],
  },
});
const emit = defineEmits(["reload"]);
const toast = useToast();

async function deleteSubcategory(subcategoryId) {
  await axiosPrivate
    .delete(`/subcategories/${subcategoryId}`)
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
  <v-container class="subcategories__container">
    <v-row>
      <h2>Subcategory List</h2>
    </v-row>
    <v-row>
      <v-table class="subcategories__table">
        <thead>
          <tr>
            <th class="text-left">Name</th>
            <th class="text-left">Slug</th>
            <th class="text-left">Category</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(subcategory, id) in props.subcategories" :key="id">
            <td :title="subcategory?.name">{{ subcategory?.name }}</td>
            <td :title="subcategory?.slug">{{ subcategory?.slug }}</td>
            <td :title="subcategory?.category?.name">{{ subcategory?.category?.name }}</td>
            <td class="d-flex align-center">
              <v-btn
                color="red"
                icon="mdi-delete"
                size="32"
                title="Delete"
                alt="Delete"
                @click="deleteSubcategory(subcategory.id)"
              />
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
  </v-container>
</template>

<style scoped>
.subcategories__table {
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
