/*
 * Copyright (C) 2023 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.example.android.trackr.ui.expense

import androidx.lifecycle.ViewModel
import androidx.lifecycle.viewModelScope
import com.example.android.trackr.data.ExpenseItem
import com.example.android.trackr.db.dao.ExpenseDao
import dagger.hilt.android.lifecycle.HiltViewModel
import kotlinx.coroutines.flow.SharingStarted
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.stateIn
import kotlinx.coroutines.launch
import javax.inject.Inject

@HiltViewModel
class ExpenseViewModel @Inject constructor(
    private val expenseDao: ExpenseDao
) : ViewModel() {

    val expenses: StateFlow<List<ExpenseItem>> = expenseDao.observeExpenses()
        .stateIn(
            scope = viewModelScope,
            started = SharingStarted.WhileSubscribed(5_000),
            initialValue = emptyList()
        )

    fun uploadExpense(expense: ExpenseItem) {
        viewModelScope.launch {
            expenseDao.updateExpense(expense.copy(isUploaded = true))
        }
    }

    fun addExpense(title: String, amount: Double, category: String, receiptUri: String?, locationInfo: String?) {
        viewModelScope.launch {
            expenseDao.insertExpense(
                ExpenseItem(
                    title = title,
                    amount = amount,
                    category = category,
                    receiptPhotoUri = receiptUri,
                    isUploaded = false,
                    locationInfo = locationInfo
                )
            )
        }
    }
}
