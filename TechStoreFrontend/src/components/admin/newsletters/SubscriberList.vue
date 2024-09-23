<script setup>
import { computed, onMounted, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";
import ConfirmationDialog from "@/components/common/ConfirmationDialog.vue";

const subscribers = ref([]);
const toast = useToast();
const selectedItemEmail = ref(null);
const showDialog = ref(false);
const searchQuery = ref("");
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 10,
});

onMounted(() => reloadSubscribers());

async function reloadSubscribers() {
  const resp = await axiosPrivate.get("/newsletters").catch((error) => console.log(error));

  if (resp?.status !== 200) return;

  subscribers.value = resp.data;
}

const openConfirmationDialog = (email) => {
  selectedItemEmail.value = email;
  showDialog.value = true;
};

const unsubscribe = async (subscriberEmail) => {
  await axiosPrivate
    .delete(`/newsletters/${subscriberEmail}`)
    .then((resp) => {
      if (resp?.status === 200) {
        toast.success(ITEM_UPDATE_SUCCESS);
      }
    })
    .catch(() => toast.error(ITEM_UPDATE_FAIL));
  await reloadSubscribers();
};

const filteredSubscribers = computed(() => {
  return subscribers.value.filter((subscriber) =>
    subscriber?.email.toLowerCase().includes(searchQuery.value.trim().toLowerCase())
  );
});

// Watch for changes in the  subscribers array and adjust the current page if necessary
// This ensures that if items are deleted on the current page and it becomes empty,
// the user is redirected to the previous valid page.
watch(
  () => subscribers.value,
  () => {
    const totalItems = filteredSubscribers?.value.length;
    const maxPage = Math.ceil(totalItems / pageState.value.itemsPerPage);

    if (pageState.value.currentPage > maxPage) {
      pageState.value.currentPage = maxPage || 1;
    }
  },
  { immediate: true, deep: true }
);

const totalPageCount = computed(() =>
  Math.ceil(filteredSubscribers.value.length / pageState.value.itemsPerPage)
);

const currentPageItems = computed(() => {
  return filteredSubscribers.value.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>

<template>
  <v-container class="dashboard__container">
    <v-row class="header-search">
      <h2>Subscriber List</h2>
      <v-text-field
        v-model="searchQuery"
        class="header-search__input"
        label="Search by email..."
        hide-details="true"
        density="compact"
        variant="outlined"
      />
    </v-row>
    <v-row>
      <v-table class="dashboard__table">
        <thead>
          <tr>
            <th class="text-left">Email</th>
            <th class="text-left">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(subscriber, id) in currentPageItems" :key="id">
            <td :title="subscriber?.email">{{ subscriber?.email }}</td>
            <td class="d-flex align-center">
              <v-btn
                class="ml-2"
                color="red"
                icon="mdi-cancel"
                size="32"
                title="Unsubscribe"
                alt="Unsubscribe"
                @click="openConfirmationDialog(subscriber?.email)"
              />
            </td>
          </tr>
          <tr v-if="subscribers.length < 1">
            <td>Nothing to see here yet.</td>
          </tr>
        </tbody>
      </v-table>
      <v-container v-if="subscribers.length > pageState.itemsPerPage">
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
      :itemId="selectedItemEmail"
      @update:showDialog="showDialog = $event"
      @confirm="unsubscribe"
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
