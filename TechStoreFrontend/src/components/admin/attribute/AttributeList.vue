<script setup>
import { computed, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_DELETE_FAIL, ITEM_DELETE_SUCCESS } from "@/constants/messages/delete";
import ConfirmationDialog from "@/components/common/ConfirmationDialog.vue";

const props = defineProps({
  attributes: {
    type: Array,
    default: () => [],
  },
});
const emit = defineEmits(["reload", "edit"]);
const toast = useToast();
const selectedItemId = ref(null);
const showDialog = ref(false);
const searchQuery = ref("");
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 10,
});

const openConfirmationDialog = (attributeId) => {
  selectedItemId.value = attributeId;
  showDialog.value = true;
};

const deleteAttribute = async (attributeId) => {
  await axiosPrivate
    .delete(`/attributes/${attributeId}`)
    .then((resp) => {
      if (resp?.status === 200) {
        toast.success(ITEM_DELETE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_DELETE_FAIL));
  emit("reload");
};

const filteredAttributes = computed(() => {
  return props.attributes.filter((attribute) =>
    attribute.name.toLowerCase().includes(searchQuery.value.trim().toLowerCase())
  );
});

// Watch for changes in the attributes array and adjust the current page if necessary
// This ensures that if items are deleted on the current page and it becomes empty,
// the user is redirected to the previous valid page.
watch(
  () => props.attributes,
  () => {
    const totalItems = filteredAttributes.value.length;
    const maxPage = Math.ceil(totalItems / pageState.value.itemsPerPage);

    if (pageState.value.currentPage > maxPage) {
      pageState.value.currentPage = maxPage || 1;
    }
  },
  { immediate: true, deep: true }
);

const totalPageCount = computed(() =>
  Math.ceil(filteredAttributes.value.length / pageState.value.itemsPerPage)
);

const currentPageItems = computed(() => {
  return filteredAttributes.value.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>

<template>
  <v-container class="dashboard__container">
    <v-row class="header-search">
      <h2>Attribute List</h2>
      <v-text-field
        v-model="searchQuery"
        class="header-search__input"
        label="Search..."
        hide-details="true"
        density="compact"
        variant="outlined"
      />
    </v-row>
    <v-row>
      <v-table class="dashboard__table">
        <thead>
          <tr>
            <th class="text-left">Name</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(attribute, id) in currentPageItems" :key="id">
            <td :title="attribute?.name">{{ attribute?.name }}</td>
            <td class="d-flex align-center">
              <v-btn
                color="blue"
                icon="mdi-lead-pencil"
                size="32"
                title="Edit"
                alt="Edit"
                @click="$emit('edit', attribute?.id)"
              />
              <v-btn
                class="ml-2"
                color="red"
                icon="mdi-delete"
                size="32"
                title="Delete"
                alt="Delete"
                @click="openConfirmationDialog(attribute?.id)"
              />
            </td>
          </tr>
          <tr v-if="props.attributes.length < 1">
            <td>Nothing to see here yet. Please add new items to see them listed here.</td>
          </tr>
        </tbody>
      </v-table>
      <v-container v-if="props.attributes.length > pageState.itemsPerPage">
        <v-row justify="center">
          <v-col cols="10">
            <v-container class="max-width">
              <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
            </v-container>
          </v-col>
        </v-row>
      </v-container>
    </v-row>
    <confirmation-dialog
      title="Confirm Deletion"
      content="Are you sure you want to delete this item?"
      confirmText="Delete"
      :showDialog="showDialog"
      :itemId="selectedItemId"
      @update:showDialog="showDialog = $event"
      @confirm="deleteAttribute"
    />
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
