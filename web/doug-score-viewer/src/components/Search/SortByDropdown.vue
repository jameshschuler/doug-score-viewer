<template>
  <div class="is-flex is-justify-content-flex-end is-flex-direction-column is-align-items-flex-end mb-4" v-if="hasResults">
    <div class="field">
      <div class="control is-expanded">
        <div class="select is-fullwidth" :class="{ 'is-loading': loading }">
          <select v-model="currentSortByOption" @change="updateSortByOption(($event.target! as HTMLSelectElement).value)">
            <option v-for="option in sortByOptions" :value="option.value">
              {{ option.text }}
            </option>
          </select>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed, ref } from "vue";
import { useRouter } from "vue-router";
import { sortByOptions } from "../../constants/sortOptions";
import { store } from "../../store";
import { getUrlSearchParams } from "../../utils";
import { isNullEmptyOrWhitespace } from "../../utils/strings";

const router = useRouter();
const loading = ref<boolean>(false);

const hasResults = computed(() => store.searchResults !== null && store.searchResults.dougScores.length !== 0);

const currentSortByOption = ref<string>(store.getCurrentSortByOption());
computed(() => {
  currentSortByOption.value = store.getCurrentSortByOption();
});

async function updateSortByOption(sortByValue: string) {
  if (store.currentSearchQuery !== null) {
    loading.value = true;
    await store.searchDougScores({
      ...store.currentSearchQuery,
      sortByOption: sortByValue,
    });

    const params = getUrlSearchParams(store.currentSearchQuery);
    if (isNullEmptyOrWhitespace(params)) {
      await router.push("/search/results");
    } else {
      await router.push(`/search/results${params}`);
    }

    loading.value = false;
  }
}
</script>
<style lang="scss"></style>
